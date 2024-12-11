using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Windows
{
	public interface IWindowService
	{
		void Open(WindowId windowId);
		void OpenOnly(WindowId windowId);
		void Close(WindowId windowId);
		void CloseAll();
		void Create(Transform parent, params WindowId[] ids);
		void Create(Transform parent, IEnumerable<WindowId> ids);
		bool TryRegistry(BaseWindow window);
	}
}