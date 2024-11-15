using Feature.DamageApplication.Component;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Feature.DamageApplication.System
{
	public sealed class CleanupSystem : IEcsRunSystem
	{
		readonly EcsWorldInject _world;
		readonly EcsFilterInject<Inc<Damage>> _damageFilter;
		readonly EcsFilterInject<Inc<DamageBuffer>> _damageBufferFilter;
		readonly EcsFilterInject<
			Inc<DamageBufferIncrementRequest>> _damageBufferIncrementRequestFilter;
		readonly EcsFilterInject<
			Inc<DamageBufferDecrementRequest>> _damageBufferDecrementRequestFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _damageFilter.Value)
				_world.Value.GetPool<Damage>().Del(e);
			foreach (var e in _damageBufferFilter.Value)
				_world.Value.GetPool<DamageBuffer>().Del(e);
			foreach (var e in _damageBufferIncrementRequestFilter.Value)
				_world.Value.GetPool<DamageBufferIncrementRequest>().Del(e);
			foreach (var e in _damageBufferDecrementRequestFilter.Value)
				_world.Value.GetPool<DamageBufferDecrementRequest>().Del(e);
		}
	}
}