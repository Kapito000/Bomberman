using Gameplay.LevelData;
using Zenject;

namespace Infrastructure.GameStatus.State
{
	public sealed class MainMenu : State, IState
	{
		[Inject] ILevelData _levelData;

		public MainMenu(IGameStateMachine gameStateMachine) : base(gameStateMachine)
		{ }

		public void Enter()
		{
			if (Util.TryDevStart(_levelData))
				return;
		}

		public void Exit()
		{ }
	}
}