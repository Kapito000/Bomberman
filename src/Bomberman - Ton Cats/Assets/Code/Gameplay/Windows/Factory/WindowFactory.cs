using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;

namespace Gameplay.Windows.Factory
{
	public class WindowFactory : IWindowFactory
	{
		[Inject] IFactoryKit _kit;
		
		public BaseWindow CreateWindow(WindowId windowId, Transform parent)
		{
			return _kit.InstantiateService
				.Instantiate<BaseWindow>(PrefabFor(windowId), parent);
		}

		BaseWindow PrefabFor(WindowId id) =>
			_kit.AssetProvider.WindowPrefab(id);
	}
}