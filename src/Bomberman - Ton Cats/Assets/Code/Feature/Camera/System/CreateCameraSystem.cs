using Common.Component;
using Extensions;
using Feature.Camera.Factory;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Camera.System
{
	public sealed class CreateCameraSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		[Inject] ICameraFactory _cameraFactory;

		readonly EcsFilterInject<Inc<Hero.Component.HeroComponent, TransformComponent>> _filter;

		public void Run(IEcsSystems systems)
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