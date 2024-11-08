using System.Collections.Generic;

namespace Animation
{
	public abstract class AnimationStateMachine<TKey>
		where TKey : struct
	{
		protected TKey _currentState;
		protected Dictionary<TKey, IAnimationState> _states;

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

		protected IAnimationState CurrentState() =>
			_states[_currentState];

		protected abstract bool IsEquals(TKey x, TKey y);
	}
}