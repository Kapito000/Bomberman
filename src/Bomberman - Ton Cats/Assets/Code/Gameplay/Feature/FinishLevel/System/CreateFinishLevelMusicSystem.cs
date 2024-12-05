using Common.Component;
using Gameplay.Feature.FinishLevel.Component;
using Gameplay.Feature.FinishLevel.Factory;
using Gameplay.Feature.GameMusic.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.FinishLevel.System
{
	public sealed class CreateFinishLevelMusicSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _parent;
		[Inject] IFinishLevelFactory _finishLevelFactory;
		
		readonly EcsFilterInject<
			Inc<FinishLevelObserver>> _observerFilter;
		readonly EcsFilterInject<
			Inc<MusicParent, TransformComponent>> _parentFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var observer in _observerFilter.Value)
			foreach (var parentEntity in _parentFilter.Value)
			{
				_parent.SetEntity(parentEntity);
				var parent = _parent.Transform();
				
				_finishLevelFactory.CreateFinishLevelMusic(parent);
			}
		}
	}
}