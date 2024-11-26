using Infrastructure.Factory;

namespace Gameplay.Feature.UI.Factory
{
	public interface IUiFactory : IFactory
	{
		int CreateRootCanvas();
		void EventSystem();
	}
}