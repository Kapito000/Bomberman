using Factory;

namespace Feature.UI.Factory
{
	public interface IUiFactory : IFactory
	{
		int CreateRootCanvas();
		void EventSystem();
	}
}