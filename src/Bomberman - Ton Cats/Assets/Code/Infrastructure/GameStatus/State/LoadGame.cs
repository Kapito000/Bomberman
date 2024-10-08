namespace Infrastructure.GameStatus.State
{
	public sealed class LoadGame : State, IState
	{
		public LoadGame(IGameStateMachine gameStateMachine) : base(gameStateMachine)
		{ }

		public string FirstScene { get; set; }

		public void Enter()
		{
			_gameStateMachine.EnterToLoadScene(FirstScene);
		}

		public void Exit()
		{ }
	}
}