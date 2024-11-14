using Feature.DamageApplication.Component;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Feature.DamageApplication.System
{
	public sealed class CleanupSystem : IEcsRunSystem
	{
		readonly EcsWorldInject _world;
		readonly EcsFilterInject<Inc<Damage>> _damageFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _damageFilter.Value)
				_world.Value.GetPool<Damage>().Del(e);
		}
	}
}