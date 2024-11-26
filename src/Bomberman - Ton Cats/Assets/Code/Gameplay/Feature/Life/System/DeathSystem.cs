using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Life.System
{
	public sealed class DeathSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _life;

		readonly EcsFilterInject<Inc<LifePoints>> _lifeFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _lifeFilter.Value)
			{
				_life.SetEntity(e);
				var lifePoints = _life.LifePoints();
				var minLifePoints = Constant.Life.c_MinLifePoints;
				if (lifePoints <= minLifePoints)
				{
					_life.Replace<Dead>();
					_life.Replace<DeathProcessor>();
				}
			}
		}
	}
}