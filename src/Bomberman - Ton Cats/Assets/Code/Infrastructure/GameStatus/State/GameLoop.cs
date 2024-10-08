namespace Infrastructure.GameStatus.State
{
	public sealed class GameLoop : State, IState
	{
		public GameLoop(IGameStateMachine gameStateMachine) :
			base(gameStateMachine)
		{ }

		public void Enter()
		{ }

		public void Exit()
		{ }
	}
}