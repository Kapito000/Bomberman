using Infrastructure.Factory;

namespace Gameplay.Feature.GameMusic.Factory
{
	public interface IGameMusicFactory : IFactory
	{
		int CreateGameMusic();
	}
}