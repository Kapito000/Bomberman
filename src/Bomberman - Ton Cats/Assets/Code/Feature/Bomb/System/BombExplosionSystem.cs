using Common.Component;
using Feature.Destruction.Component;
using Feature.Explosion.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Feature.Bomb.System
{
	public sealed class BombExplosionSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _bomb;
		[Inject] IExplosionFactory _explosionFactory;

		readonly EcsFilterInject<
			Inc<Component.Bomb, Explosion.Component.Explosion, TransformComponent>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				_bomb.SetEntity(e);
				Vector2 pos = _bomb.TransformPos();
				_explosionFactory.CreateExplosionRequest(pos);
				_bomb.Add<Destructed>();
			}
		}
	}
}