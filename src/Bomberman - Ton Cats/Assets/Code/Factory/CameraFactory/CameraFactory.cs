using Factory.EntityBehaviourFactory;
using Infrastructure.AssetProvider;
using Zenject;
using IInstantiator = InstantiateService.IInstantiator;

namespace Factory.CameraFactory
{
	public sealed class CameraFactory : ICameraFactory
	{
		[Inject] IInstantiator _instantiator;
		[Inject] IAssetProvider _assetProvider;
		[Inject] IEntityBehaviourFactory _entityBehaviourFactory;

		public int CreateCamera()
		{
			var cameraPrefab = _assetProvider.Camera();
			var cameraInstance = _instantiator.Instantiate(cameraPrefab);
			return _entityBehaviourFactory.CreateEntityBehaviour(cameraInstance);
		}

		public int CreateVirtualCamera()
		{
			var prefab = _assetProvider.VirtualCamera();
			var cameraInstance = _instantiator.Instantiate(prefab);
			return _entityBehaviourFactory.CreateEntityBehaviour(cameraInstance);
		}
	}
}