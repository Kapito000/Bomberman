using Common.Component;
using Feature.DamageApplication.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.DamageApplication.System
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
				var lifePoints = _alive.LifePoints();
				_alive.SetLifePoints(lifePoints - damage);
				_alive.SubtractDamage(damage);
			}
		}
	}
}