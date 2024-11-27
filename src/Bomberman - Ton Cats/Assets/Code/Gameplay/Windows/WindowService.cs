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
			if (_openedWindows.ContainsKey(windowId))
				return;

			if (_poolWindows.TryGetValue(windowId, out var window) == false)
			{
				Debug.LogWarning($"Cannot open \"{windowId}\" windows.");
				return;
			}

			window.Show();
			_openedWindows[windowId] = window;
		}

		public void Close(WindowId windowId)
		{
			if (_openedWindows.TryGetValue(windowId, out var window) == false)
			{
				Debug.LogWarning($"Cannot close \"{windowId}\" windows.");
				return;
			}

			window.Hide();
			_openedWindows.Remove(windowId);
		}
	}
}