namespace Infrastructure.GameStatus.State
{
	public sealed class GameExit : State, IState
	{
		public GameExit(IGameStateMachine gameStateMachine) : base(gameStateMachine)
		{ }

		public void Enter()
		{
			UnityEngine.Application.Quit();
		}

		public void Exit()
		{ }
	}
}