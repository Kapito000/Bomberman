﻿using Extensions;
using Gameplay.Feature.Explosion.Component;
using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Explosion.System
{
	public sealed class ExplosionDamageSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _target;
		[Inject] EntityWrapper _explosion;

		readonly EcsFilterInject<
			Inc<Component.Explosion, TargetsBuffer>> _explosionFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var explosion in _explosionFilter.Value)
			{
				_explosion.SetEntity(explosion);
				foreach (var pack in _explosion.TargetsBuffer())
				{
					if (pack.Unpack(out var target) == false)
						continue;

					_target.SetEntity(target);
					if (_target.Has<LifePoints>() == false)
						continue;

					if (_explosion.Has<ExplodedTargetsBuffer>() &&
					    _explosion.ExplodedTargetsBuffer().Contains(pack))
						continue;

					_target.AppendDamage(Constant.Damage.c_Default);
					_explosion.ReplaceExplodedTargetsBuffer(target);
				}
			}
		}
	}
}