using Common.Component;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Input.Component;
using Gameplay.Physics;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class RemoteDetonationSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _input;
		[Inject] EntityWrapper _camera;
		[Inject] EntityWrapper _entity;
		[Inject] IPhysicsService _physicsService;

		readonly EcsFilterInject<
			Inc<InputReader, CharacterInput, ScreenTap, BombCarrier>> _inputFilter;
		readonly EcsFilterInject<
			Inc<CameraComponent>> _cameraFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var inputEntity in _inputFilter.Value)
			foreach (var cameraEntity in _cameraFilter.Value)
			{
				_input.SetEntity(inputEntity);
				_camera.SetEntity(cameraEntity);

				// Tap point.
				var camera = _camera.Camera();
				var screenTap = _input.ScreenTap();
				var tapPoint = camera.ScreenToWorldPoint(screenTap);

				foreach (var e in _physicsService.OverlapPoint(tapPoint))
				{
					_entity.SetEntity(e);
					if (_entity.Has<BombRemoteDetonation>() ==false)
						continue;

					_entity.Replace<BombExplosion>();
				}
			}
		}
	}
}