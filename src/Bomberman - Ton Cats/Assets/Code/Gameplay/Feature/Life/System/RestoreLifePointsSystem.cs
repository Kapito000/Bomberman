using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Life.System
{
	public sealed class RestoreLifePointsSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _entity;

		readonly EcsFilterInject<Inc<RestoreLifePoints>, Exc<Immortal>>
			_restoreLifePointsFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _restoreLifePointsFilter.Value)
			{
				_entity.SetEntity(e);
				var lifePoints = _entity.RestoreLifePoints();
				_entity.Remove<RestoreLifePoints>();
				_entity.AddLifePoints(lifePoints);
			}
		}
	}
}