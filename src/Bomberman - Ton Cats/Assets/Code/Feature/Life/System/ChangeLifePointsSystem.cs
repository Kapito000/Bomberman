using Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Life.System
{
	public sealed class ChangeLifePointsSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _life;

		readonly EcsFilterInject<Inc<LifePoints, ChangeLifePoints>> _lifeFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var life in _lifeFilter.Value)
			{
				_life.SetEntity(life);
				var points = _life.LifePoints();
				var change = _life.ChangeLifePoints();
				_life.SetLifePoints(points + change);
			}
		}
	}
}