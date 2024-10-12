using Factory.Kit;

namespace Factory.UiFactory
{
	public interface IUiFactory : IFactory
	{
		int CreateRootCanvas();
	}
}