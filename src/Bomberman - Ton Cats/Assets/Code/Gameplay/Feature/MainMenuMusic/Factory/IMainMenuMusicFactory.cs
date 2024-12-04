using Infrastructure.Factory;

namespace Gameplay.Feature.MainMenuMusic.Factory
{
	public interface IMainMenuMusicFactory : IFactory
	{
		int CreateAmbientMusic();
	}
}