using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Windows.Factory
{
	public interface IWindowFactory : IFactory
	{
		public BaseWindow CreateWindow(WindowId windowId, Transform parent);
	}
}