using System.Collections.Generic;

namespace Common.BaseStateMachine
{
	public abstract class StateMachine<TKey, TState> : IStateMachine<TKey, TState>
		where TState : IState
	{
		protected TKey _currentState;
		protected Dictionary<TKey, TState> _states;

		public void Init(TKey initState)
		{
			_currentState = initState;
			_states[_currentState].Enter();
		}

		public void Enter(TKey newState)
		{
			if (IsEquals(_currentState, newState))
				return;

			CurrentState().Exit();
			_currentState = newState;
			CurrentState().Enter();
		}

		protected TState CurrentState() =>
			_states[_currentState];

		protected abstract bool IsEquals(TKey x, TKey y);
	}
}