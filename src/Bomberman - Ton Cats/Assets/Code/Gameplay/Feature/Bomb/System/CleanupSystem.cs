using Gameplay.Feature.Bomb.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class CleanupSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _entity;
		
		readonly EcsFilterInject<Inc<CreateExplosionRequest>> _filter;
		
		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_entity.SetEntity(e);
				_entity.Destroy();
			}
		}
	}
}