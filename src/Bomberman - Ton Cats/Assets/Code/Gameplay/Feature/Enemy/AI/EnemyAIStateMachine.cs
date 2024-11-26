using System.Collections.Generic;
using Common.BaseStateMachine;
using Gameplay.Feature.Enemy.AI.States;

namespace Gameplay.Feature.Enemy.AI
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