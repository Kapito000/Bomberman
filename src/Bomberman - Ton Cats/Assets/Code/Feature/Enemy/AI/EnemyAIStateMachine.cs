using System.Collections.Generic;
using BaseStateMachine;
using Feature.Enemy.AI.States;

namespace Feature.Enemy.AI
{
	public sealed class EnemyAIStateMachine : StateMachine<State, IEnemyAIState>,
		IEnemyAIStateMachine
	{
		public EnemyAIStateMachine()
		{
			_states = new Dictionary<State, IEnemyAIState>()
			{
				{ State.Movement, new Movement() },
			};
		}

		protected override bool IsEquals(State x, State y) => x == y;
	}
}