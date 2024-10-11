using Common.Component;
using Extensions;
using Factory.CameraFactory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Camera.System
{
	public sealed class CreateCameraSystem : EcsSystem, IEcsInitSystem
	{
		[Inject] ICameraFactory _cameraFactory;

		readonly EcsFilterInject<Inc<Hero.Component.Hero, Transform>> _filter;

		public void Init(IEcsSystems systems)
		{
			foreach (var heroEntity in _filter.Value)
			{
				var cameraEntity = _cameraFactory.CreateCamera();

				var virtualCameraEntity = _cameraFactory.CreateVirtualCamera();
				var heroTransform = _world.Transform(heroEntity);
				_world.AddFollowTarget(virtualCameraEntity, heroTransform);
			}
		}
	}
}