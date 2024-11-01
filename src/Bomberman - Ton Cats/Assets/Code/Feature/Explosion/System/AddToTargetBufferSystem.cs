using Feature.Explosion.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Explosion.System
{
	public sealed class AddToTargetBufferSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _buffer;
		[Inject] EntityWrapper _explosion;

		readonly EcsFilterInject<
			Inc<Component.Explosion, TargetsBufferIncrementRequest>> _requestFilter;
		readonly EcsFilterInject<
			Inc<Component.Explosion, TargetsBuffer>> _targetBufferFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var request in _requestFilter.Value)
			{
				_explosion.SetEntity(request);
				var others = _explosion.TargetsBufferIncrementRequest();
				foreach (var other in others)
				{
					if (HasInOtherBuffers(other))
						continue;
					_explosion.ReplaceTargetsBuffer(other);
				}
				others.Clear();
			}
		}

		bool HasInOtherBuffers(int otherEntity)
		{
			foreach (var buffer in _targetBufferFilter.Value)
			{
				_buffer.SetEntity(buffer);
				if (_buffer.HasInTargetBuffer(otherEntity))
					return true;
			}
			return false;
		}
	}
}