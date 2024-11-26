using Infrastructure.Factory;

namespace Gameplay.EndGame.Factory
{
	public interface IFinishLevelFactory : IFactory
	{
		int CreateFinishLevelObserver();
	}
}