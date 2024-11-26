using Common;
using Infrastructure.GameStatus.State;
using Zenject;

namespace Infrastructure.GameStatus
{
	public sealed class GameStateMachine : IGameStateMachine
	{
		IState _activeState;
		readonly TypeLocator<IState> _states = new();

		[Inject]
		void Construct(IState[] states)
		{
			_states.RegisterRange(states);
		}

		public void Enter<TState>() where TState : class, IState
		{
			_activeState?.Exit();
			TState state = GetState<TState>();
			_activeState = state;
			state.Enter();
		}

		public TState GetState<TState>() where TState : class, IState =>
			_states[typeof(TState)] as TState;
	}
}