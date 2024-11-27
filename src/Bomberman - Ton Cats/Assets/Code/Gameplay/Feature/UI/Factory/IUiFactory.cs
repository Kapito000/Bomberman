using Infrastructure.Factory;
using Transform = UnityEngine.Transform;

namespace Gameplay.Feature.UI.Factory
{
	public interface IUiFactory : IFactory
	{
		int CreateRootCanvas();
		int WindowsRoot(Transform parent);
		void EventSystem();
	}
}