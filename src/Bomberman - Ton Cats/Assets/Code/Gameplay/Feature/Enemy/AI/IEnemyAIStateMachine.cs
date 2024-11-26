using Common.BaseStateMachine;

namespace Gameplay.Feature.Enemy.AI
{
	public interface IEnemyAIStateMachine : IStateMachine<State, IEnemyAIState>
	{ }
}