using System.Collections.Generic;
using Gameplay.StaticData;
using Gameplay.Windows;

namespace Gameplay.UI.StaticData
{
	public interface IWindowKitData : IStaticData
	{
		bool TryGetKit(WindowKitId kitId, out IReadOnlyList<WindowId> list);
	}
}