using Infrastructure.AssetProvider;
using UnityEngine;
using Zenject;
using IInstantiator = InstantiateService.IInstantiator;

namespace Factory.CameraFactory
{
	public sealed class CameraFactory : ICameraFactory
	{
		[Inject] IInstantiator _instantiator;
		[Inject] IAssetProvider _assetProvider;

		public GameObject CreateCamera()
		{
			var prefab = _assetProvider.Camera();
			var instance = _instantiator.Instantiate(prefab);
			return instance;
		}
	}
}