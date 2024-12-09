using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Life.System
{
	public sealed class DeathSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		[Inject] EntityWrapper _life;

		readonly EcsFilterInject<Inc<LifePoints>> _lifeFilter;
		readonly EcsFilterInject<Inc<DeathProcessor>> _deathProcessorFilter;

		public void Run(IEcsSystems systems)
		{
			Cleanup();
			
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

		void Cleanup()
		{
			foreach (var e in _deathProcessorFilter.Value)
				_world.GetPool<DeathProcessor>().Del(e);
		}
	}
}