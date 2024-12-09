using Infrastructure.Factory;
using Transform = UnityEngine.Transform;

namespace Gameplay.UI.Factory
{
	public interface IUiFactory : IFactory
	{
		int CreateRootCanvas();
		int WindowsRoot(Transform parent);
		void EventSystem();
	}
}