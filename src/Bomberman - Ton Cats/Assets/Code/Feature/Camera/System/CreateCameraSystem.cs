using Common;
using Common.Component;
using Extensions;
using Feature.Camera.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Camera.System
{
	public sealed class CreateCameraSystem : EcsSystem, IEcsInitSystem
	{
		[Inject] ICameraFactory _cameraFactory;

		readonly EcsFilterInject<Inc<Hero.Component.Hero, TransformComponent>> _filter;

		public void Init(IEcsSystems systems)
		{
			foreach (var heroEntity in _filter.Value)
			{
				_cameraFactory.CreateCamera();

				var heroTransform = _world.Transform(heroEntity);
				var virtualCameraEntity = _cameraFactory.CreateVirtualCamera(heroTransform);
			}
		}
	}
}