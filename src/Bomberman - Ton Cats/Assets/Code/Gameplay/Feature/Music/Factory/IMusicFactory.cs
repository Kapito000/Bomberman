using Infrastructure.Factory;

namespace Gameplay.Feature.Music.Factory
{
	public interface IMusicFactory : IFactory
	{
		int CreateGameMusic();
	}
}