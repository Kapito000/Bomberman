using Gameplay.StaticData.LevelData;
using Zenject;

namespace Infrastructure.GameStatus.State
{
	public sealed class LoadProject : State, IState
	{
		[Inject] ILevelsData _levelsData;

		public LoadProject(IGameStateMachine gameStateMachine) : base(
			gameStateMachine)
		{ }

		public string FirstScene { get; set; }

		public void Enter()
		{
			_levelsData.Init();
			_gameStateMachine.EnterToLoadScene(FirstScene);
		}

		public void Exit()
		{ }
	}
}