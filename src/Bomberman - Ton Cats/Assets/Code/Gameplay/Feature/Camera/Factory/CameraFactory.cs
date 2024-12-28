using Infrastructure.AssetProvider;
using Infrastructure.ECS;
using Infrastructure.Factory.EntityBehaviourFactory;
using Infrastructure.Factory.Kit;
using Infrastructure.InstantiateService;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Camera.Factory
{
	public sealed class CameraFactory : ICameraFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _entity;
		[Inject] IAssetProvider _assetProvider;
		[Inject] IInstantiateService _instantiateService;
		[Inject] IEntityBehaviourFactory _entityBehaviourFactory;

		public int CreateCamera()
		{
			var prefab = _assetProvider.Camera();
			var instance = _kit.InstantiateService.Instantiate(prefab);
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_entity.SetEntity(e);

			if (false == instance.TryGetComponent<UnityEngine.Camera>(out var camera))
			{
				Debug.LogError("Cannot to create camera.");
				return e;
			}

			return _entity
				.AddCamera(camera)
				.Enity;
		}

		public int CreateVirtualCamera(Transform followTarget)
		{
			var prefab = _assetProvider.VirtualCamera();
			var instance = _instantiateService.Instantiate(prefab);
			var entity = _entityBehaviourFactory.InitEntityBehaviour(instance);
			_entity.SetEntity(entity);
			_entity.AddVirtualCameraFollowTarget(followTarget);
			return entity;
		}
	}
}