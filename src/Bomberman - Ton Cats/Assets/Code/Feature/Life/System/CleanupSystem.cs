using Feature.Life.Component;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Feature.Life.System
{
	public sealed class CleanupSystem : IEcsRunSystem
	{
		readonly EcsWorldInject _world;
		readonly EcsFilterInject<Inc<ChangeLifePoints>> _eventFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _eventFilter.Value) 
				_world.Value.GetPool<ChangeLifePoints>().Del(e);
		}
	}
}