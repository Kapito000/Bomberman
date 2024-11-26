using Infrastructure;

namespace Gameplay.Feature.MainMenu
{
	public interface IMainMenuService : IService
	{
		void LaunchGame();
	}
}