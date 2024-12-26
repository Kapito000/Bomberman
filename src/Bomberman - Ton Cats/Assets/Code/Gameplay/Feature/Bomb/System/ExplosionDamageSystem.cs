using Extensions;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class ExplosionDamageSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _target;
		[Inject] EntityWrapper _explosion;

		readonly EcsFilterInject<Inc<Explosion, TargetsBuffer>> _explosionFilter;

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