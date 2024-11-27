using System.Collections.Generic;
using Gameplay.Windows.Factory;
using UnityEngine;
using Zenject;

namespace Gameplay.Windows
{
	public sealed class WindowService : IWindowService
	{
		[Inject] IWindowFactory _windowFactory;

		readonly Dictionary<WindowId, BaseWindow> _poolWindows = new();
		readonly Dictionary<WindowId, BaseWindow> _openedWindows = new();

		public void Create(Transform parent, params WindowId[] ids)
		{
			foreach (var id in ids)
			{
				if (_poolWindows.ContainsKey(id))
					continue;

				var window = _windowFactory.CreateWindow(id, parent);
				_poolWindows[id] = window;
			}
		}

		public void Open(WindowId windowId)
		{
			_openedWindows.Add(windowId, _windowFactory.CreateWindow(windowId));
		}

		public void Close(WindowId windowId)
		{
			BaseWindow window = _openedWindows[windowId];
			_openedWindows.Remove(windowId);
			Object.Destroy(window.gameObject);
		}
	}
}