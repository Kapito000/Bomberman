using Gameplay.Feature.Explosion.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Explosion.System
{
	public sealed class RemoveFromTargetBufferSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _request;
		[Inject] EntityWrapper _explosion;

		readonly EcsFilterInject<
				Inc<Component.Explosion, TargetsBuffer, TargetsBufferDecrementRequest>>
			_explosionFilter;
		readonly EcsFilterInject<
				Inc<Component.Explosion, TargetsBufferDecrementRequest>>
			_removeRequestFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var explosion in _explosionFilter.Value)
			{
				_explosion.SetEntity(explosion);
				var others = _explosion.TargetsBufferDecrementRequest();
				foreach (var other in others)
					_explosion.TargetsBufferRemove(other);
				others.Clear();
			}

			ClearRequestBuffer();
		}

		void ClearRequestBuffer()
		{
			foreach (var request in _removeRequestFilter.Value)
			{
				_request.SetEntity(request);
				_request.TargetsBufferDecrementRequest().Clear();
			}
		}
	}
}