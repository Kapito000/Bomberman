using Common.Component;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Bomb.Factory;
using Gameplay.Feature.Destruction.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class BombExplosionSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _bomb;
		[Inject] IBombFactory _factory;

		readonly EcsFilterInject<
				Inc<BombComponent, BombExplosion, ExplosionRadius, TransformComponent>>
			_bombFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var bombEntity in _bombFilter.Value)
			{
				_bomb.SetEntity(bombEntity);
				Vector2 pos = _bomb.TransformPos();
				var explosionRadius = _bomb.ExplosionRadius();
				_factory.CreateExplosionRequest(pos, explosionRadius);
				_bomb.Add<Destructed>();
			}
		}
	}
}