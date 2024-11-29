using Extensions;
using Gameplay.Feature.Collisions;
using Gameplay.Feature.Collisions.Component;
using Gameplay.Feature.FinishLevel.Component;
using Gameplay.Feature.Hero.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.FinishLevel.System
{
	public sealed class EnterToFinishLevelDoorProcessSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _door;
		[Inject] EntityWrapper _other;
		[Inject] EntityWrapper _finishLevelObserver;

		readonly EcsFilterInject<
				Inc<FinishLevelObserver>, Exc<HeroInFinishLevelDoor>>
			_finishLevelObserverFilter;
		readonly EcsFilterInject<
			Inc<FinishLevelDoor, TriggerEnterBuffer>> _finishLevelDoorFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var observer in _finishLevelObserverFilter.Value)
			foreach (var door in _finishLevelDoorFilter.Value)
			{
				_door.SetEntity(door);
				_finishLevelObserver.SetEntity(observer);

				var triggerEnterBuffer = _door.TriggerEnterBuffer();
				foreach (var pack in triggerEnterBuffer)
				{
					if (HeroEnteredIntoDoor(pack) == false)
						continue;

					_finishLevelObserver.Replace<HeroInFinishLevelDoor>();
				}
			}
		}

		bool HeroEnteredIntoDoor(EcsPackedEntityWithWorld pack)
		{
			if (pack.Unpack(out var otherEntity) == false)
				return false;

			_other.SetEntity(otherEntity);
			if (_other.Has<HeroComponent>() == false)
				return false;
			
			return true;
		}
	}
}