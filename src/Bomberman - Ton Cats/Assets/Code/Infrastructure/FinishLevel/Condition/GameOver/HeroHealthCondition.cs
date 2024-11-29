using Gameplay.Feature.FinishLevel.Component;
using Infrastructure.ECS;
using Zenject;

namespace Infrastructure.FinishLevel.Condition.GameOver
{
	public sealed class HeroHealthCondition : IGameOverCondition
	{
		[Inject] EntityWrapper _observer;

		public bool Check(int observerEntity)
		{
			_observer.SetEntity(observerEntity);
			return _observer.Has<HeroDead>();
		}
	}
}