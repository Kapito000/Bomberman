using UnityEngine;

namespace Gameplay.Windows
{
	public interface IWindowService
	{
		void Open(WindowId windowId);
		void Close(WindowId windowId);
		void Create(Transform parent, params WindowId[] ids);
	}
}