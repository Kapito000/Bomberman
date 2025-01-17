using Extensions;
using Gameplay.Feature.Bonus.Component;
using Gameplay.Feature.Bonus.StaticData;
using Gameplay.Feature.Collisions;
using Gameplay.Feature.Collisions.Component;
using Gameplay.Feature.Hero.Component;
using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.BonusApplication.System
{
	public sealed class AddLifeBonusApplicationSystem : IEcsRunSystem
	{
		[Inject] IBonusNames _bonusNames;
		[Inject] EntityWrapper _bonus;
		[Inject] EntityWrapper _other;

		readonly EcsFilterInject<
			Inc<BonusComponent, BonusType, TriggerEnterBuffer>> _bonusFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var bonusEntity in _bonusFilter.Value)
			{
				_bonus.SetEntity(bonusEntity);
				var bonusType = _bonus.BonusType();
				if (bonusType != _bonusNames.AddLifePoint)
					continue;

				var enterBuffer = _bonus.TriggerEnterBuffer();
				foreach (var otherPackedEntity in enterBuffer)
				{
					if (otherPackedEntity.Unpack(out int otherEntity) == false)
					{
						Debug.LogError("Cannot to unpack entity");
						continue;
					}

					_other.SetEntity(otherEntity);
					if (_other.Has<HeroComponent, LifePoints>() == false)
						continue;

					_bonus.Destroy();
					_other.AppendChangeLifePoints(1);
					break;
				}
			}
		}
	}
}