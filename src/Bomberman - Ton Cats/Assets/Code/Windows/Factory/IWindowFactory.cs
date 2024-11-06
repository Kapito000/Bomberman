using Factory;
using UnityEngine;

namespace Windows.Factory
{
	public interface IWindowFactory : IFactory
	{
		public void SetUIRoot(RectTransform uiRoot);
		public BaseWindow CreateWindow(WindowId windowId);
	}
}