using Feature.DamageApplication.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.DamageApplication.System
{
	public sealed class UpdateDamageBufferSystemSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		[Inject] EntityWrapper _buffer;

		readonly EcsFilterInject<
			Inc<DamageBufferIncrementRequest>> _damageIncrementRequestFilter;
		readonly EcsFilterInject<
			Inc<DamageBufferDecrementRequest>> _damageDecrementRequestFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _damageIncrementRequestFilter.Value)
			{
				_buffer.SetEntity(request);

				var incrementRequest = _buffer.DamageBufferIncrementRequest();
				foreach (var otherEntity in incrementRequest)
					_buffer.ReplaceToDamageBuffer(otherEntity);
			}

			foreach (var request in _damageDecrementRequestFilter.Value)
			{
				_buffer.SetEntity(request);

				var decrementRequest = _buffer.DamageBufferDecrementRequest();
				foreach (var otherEntity in decrementRequest)
					_buffer.RemoveFromDamageBuffer(otherEntity);
			}
		}
	}
}