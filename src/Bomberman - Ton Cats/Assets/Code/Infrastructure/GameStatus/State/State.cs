namespace Infrastructure.GameStatus.State
{
	public abstract class State
	{
		protected readonly IGameStateMachine _gameStateMachine;

		protected State(IGameStateMachine gameStateMachine)
		{
			_gameStateMachine = gameStateMachine;
		}
	}
}