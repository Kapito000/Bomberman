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

		readonly EcsFilterInject<Inc<Hero.Component.Hero>> _filter;

		public void Init(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{
				var cameraObj = _cameraFactory.CreateCamera();
				cameraObj.TryAddToEcs(_world, out var cameraEntity);
			}
		}
	}
}