using Gameplay.Feature.Explosion.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Explosion.System
{
	public sealed class CreateExplosionRequestCleanupSystem : IEcsRunSystem
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