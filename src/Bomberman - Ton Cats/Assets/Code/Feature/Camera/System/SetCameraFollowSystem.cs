using Extensions;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Feature.Camera.System
{
	public sealed class SetCameraFollowSystem : EcsSystem, IEcsRunSystem
	{
		readonly EcsFilterInject<Inc<VirtualCamera, FollowTarget>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				var target = _world.FollowTarget(e);
				_world.VirtualCamera(e).Follow = target;
				_world.Remove<FollowTarget>(e);
			}
		}
	}
}