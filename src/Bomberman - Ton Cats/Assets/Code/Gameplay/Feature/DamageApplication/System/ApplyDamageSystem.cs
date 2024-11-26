using Gameplay.Feature.DamageApplication.Component;
using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.DamageApplication.System
{
	public sealed class ApplyDamageSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _alive;

		readonly EcsFilterInject<Inc<LifePoints, Damage>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var alive in _filter.Value)
			{
				_alive.SetEntity(alive);
				
				var damage = _alive.Damage();
				_alive.AppendChangeLifePoints(-damage);
			}
		}
	}
}