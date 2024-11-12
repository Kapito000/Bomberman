using BaseStateMachine;

namespace Feature.Enemy.AI
{
	public interface IEnemyAIStateMachine : IStateMachine<State, IEnemyAIState>
	{ }
}