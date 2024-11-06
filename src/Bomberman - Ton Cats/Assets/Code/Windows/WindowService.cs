using System.Collections.Generic;
using Windows.Factory;
using UnityEngine;
using Zenject;

namespace Windows
{
	public sealed class WindowService : IWindowService
	{
		[Inject] IWindowFactory _windowFactory;

		readonly List<BaseWindow> _openedWindows = new();

		public void Open(WindowId windowId) =>
			_openedWindows.Add(_windowFactory.CreateWindow(windowId));

		public void Close(WindowId windowId)
		{
			BaseWindow window = _openedWindows.Find(x => x.Id == windowId);
			_openedWindows.Remove(window);
			Object.Destroy(window.gameObject);
		}
	}
}