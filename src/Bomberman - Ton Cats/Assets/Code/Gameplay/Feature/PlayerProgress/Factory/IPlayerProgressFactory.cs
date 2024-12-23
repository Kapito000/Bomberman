using Infrastructure.Factory;

namespace Gameplay.Feature.PlayerProgress.Factory
{
	public interface IPlayerProgressFactory : IFactory
	{
		int CreatePlayerProgress();
	}
}