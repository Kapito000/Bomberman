using Infrastructure.Factory;

namespace Gameplay.FinishLevel.Factory
{
	public interface IFinishLevelFactory : IFactory
	{
		int CreateFinishLevelObserver();
	}
}