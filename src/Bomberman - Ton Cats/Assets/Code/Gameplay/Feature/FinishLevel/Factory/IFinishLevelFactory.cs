using Infrastructure.Factory;

namespace Gameplay.Feature.FinishLevel.Factory
{
	public interface IFinishLevelFactory : IFactory
	{
		int CreateFinishLevelObserver();
	}
}