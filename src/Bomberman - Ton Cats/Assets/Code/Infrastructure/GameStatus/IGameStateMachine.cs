using Infrastructure.GameStatus.State;

namespace Infrastructure.GameStatus
{
	public interface IGameStateMachine
	{
		void Enter<TState>() where TState : class, IState;
		TState GetState<TState>() where TState : class, IState;
	}
}