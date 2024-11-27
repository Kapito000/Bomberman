using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;

namespace Gameplay.Windows.Factory
{
	public class WindowFactory : IWindowFactory
	{
		[Inject] IFactoryKit _kit;
		
		RectTransform _uiRoot;

		public void SetUIRoot(RectTransform uiRoot) =>
			_uiRoot = uiRoot;

		public BaseWindow CreateWindow(WindowId windowId)
		{
			return _kit.InstantiateService
				.Instantiate<BaseWindow>(PrefabFor(windowId), _uiRoot);
		}

		BaseWindow PrefabFor(WindowId id) =>
			_kit.AssetProvider.WindowPrefab(id);
	}
}