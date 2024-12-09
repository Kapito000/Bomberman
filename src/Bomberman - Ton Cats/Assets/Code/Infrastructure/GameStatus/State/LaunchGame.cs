using Gameplay.LevelData;
using Zenject;

namespace Infrastructure.GameStatus.State
{
	public sealed class LaunchGame : State, IState
	{
		[Inject] ILevelData _levelData;

		public LaunchGame(IGameStateMachine gameStateMachine) : base(
			gameStateMachine)
		{ }

		public void Enter()
		{
			if (Util.TryDevStart(_levelData))
				return;

			_levelData.EcsRunner.InitWorld();
			_levelData.AudioSourcePool.ExpandBy(Constant.Value.c_AudioSourcePoolSize);
			_gameStateMachine.Enter<GameLoop>();
		}

		public void Exit()
		{ }
	}
}