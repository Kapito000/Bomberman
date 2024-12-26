using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Bomb.Factory;
using Gameplay.Feature.Destruction.Component;
using Gameplay.Feature.Map.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class BombExplosionSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _bomb;
		[Inject] IBombFactory _factory;

		readonly EcsFilterInject<
				Inc<BombComponent, BombExplosion, ExplosionRadius, CellPos>>
			_bombFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var bombEntity in _bombFilter.Value)
			{
				_bomb.SetEntity(bombEntity);
				var cell = _bomb.CellPos();
				var explosionRadius = _bomb.ExplosionRadius();
				_factory.CreateCallExplosion(cell, explosionRadius);
				_bomb.Add<Destructed>();
			}
		}
	}
}