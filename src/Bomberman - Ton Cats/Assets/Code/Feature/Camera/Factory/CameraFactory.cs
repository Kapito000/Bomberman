﻿using Factory.EntityBehaviourFactory;
using Infrastructure.AssetProvider;
using Infrastructure.ECS;
using InstantiateService;
using UnityEngine;
using Zenject;

namespace Feature.Camera.Factory
{
	public sealed class CameraFactory : ICameraFactory
	{
		[Inject] IAssetProvider _assetProvider;
		[Inject] IInstantiateService _instantiateService;
		[Inject] IEntityBehaviourFactory _entityBehaviourFactory;
		[Inject] EntityWrapper _vCameraEntity;

		public void CreateCamera()
		{
			var cameraPrefab = _assetProvider.Camera();
			var cameraInstance = _instantiateService.Instantiate(cameraPrefab);
		}

		public int CreateVirtualCamera(Transform followTarget)
		{
			var prefab = _assetProvider.VirtualCamera();
			var instance = _instantiateService.Instantiate(prefab);
			var entity = _entityBehaviourFactory.InitEntityBehaviour(instance);
			_vCameraEntity.SetEntity(entity);
			_vCameraEntity.AddVirtualCameraFollowTarget(followTarget);
			return entity;
		}
	}
}