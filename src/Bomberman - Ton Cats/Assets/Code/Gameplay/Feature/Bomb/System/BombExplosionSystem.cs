using Common.Component;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Bomb.Factory;
using Gameplay.Feature.Destruction.Component;
using Gameplay.Feature.Map.MapController;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class BombExplosionSystem : IEcsRunSystem
	{
		[Inject] IBombFactory _factory;
		[Inject] EntityWrapper _bomb;
		[Inject] IMapController _mapController;

		readonly EcsFilterInject<
				Inc<BombComponent, BombExplosion, ExplosionRadius, TransformComponent>>
			_bombFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var bombEntity in _bombFilter.Value)
			{
				_bomb.SetEntity(bombEntity);
				var explosionRadius = _bomb.ExplosionRadius();
				var pos = _bomb.TransformPos();
				var cell = _mapController.WorldToCell(pos);
				_factory.CreateCallExplosion(cell, explosionRadius);
				_bomb.Add<Destructed>();
			}
		}
	}
}