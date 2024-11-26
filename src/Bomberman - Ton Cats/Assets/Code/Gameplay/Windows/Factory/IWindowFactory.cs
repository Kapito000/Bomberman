using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Windows.Factory
{
	public interface IWindowFactory : IFactory
	{
		public void SetUIRoot(RectTransform uiRoot);
		public BaseWindow CreateWindow(WindowId windowId);
	}
}