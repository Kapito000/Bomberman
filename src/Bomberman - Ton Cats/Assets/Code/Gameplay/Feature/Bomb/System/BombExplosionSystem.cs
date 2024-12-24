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
				Inc<BombComponent, Component.Explosion, TransformComponent>>
			_filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_bomb.SetEntity(e);
				Vector2 pos = _bomb.TransformPos();
				_factory.CreateExplosionRequest(pos);
				_bomb.Add<Destructed>();
			}
		}
	}
}