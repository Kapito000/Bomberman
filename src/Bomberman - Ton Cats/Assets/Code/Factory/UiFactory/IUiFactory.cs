namespace Factory.UiFactory
{
	public interface IUiFactory : IFactory
	{
		int CreateRootCanvas();
		void EventSystem();
	}
}