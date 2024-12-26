using Extensions;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Collisions;
using Gameplay.Feature.Collisions.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class ExplosionDamage : IEcsRunSystem
	{
		[Inject] EntityWrapper _other;
		[Inject] EntityWrapper _explosion;

		readonly EcsFilterInject<Inc<Explosion, TriggerEnterBuffer>>
			_explosionFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _explosionFilter.Value)
			{
				_explosion.SetEntity(request);
				var others = _explosion.TriggerEnterBuffer();
				foreach (var other in others)
				{
					if (other.Unpack(out var otherEntity) == false)
						continue;

					_other.SetEntity(otherEntity);
					_other.AppendDamage(Constant.Damage.c_Default);
				}
				others.Clear();
			}
		}
	}
}