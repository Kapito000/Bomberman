﻿using Feature.Bomb.Component;
using Feature.Bomb.Factory;
using Feature.Destruction.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;
using Transform = Common.Component.Transform;

namespace Feature.Bomb.System
{
	public sealed class BombExplosionSystem : EcsSystem, IEcsRunSystem
	{
		[Inject] EntityWrapper _bomb;
		[Inject] IExplosionFactory _explosionFactory;

		readonly EcsFilterInject<
			Inc<Component.Bomb, Explosion, Transform>> _filter;

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