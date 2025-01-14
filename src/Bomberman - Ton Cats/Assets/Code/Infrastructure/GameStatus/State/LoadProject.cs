using Gameplay.Feature.Bomb.StaticData;
using Gameplay.SaveLoad;
using Gameplay.StaticData.LevelData;
using Zenject;

namespace Infrastructure.GameStatus.State
{
	public sealed class LoadProject : State, IState
	{
		[Inject] IEnemiesAtLevelsData _enemiesAtLevelsData;
		[Inject] IBombDataService _bombData;
		[Inject] ISaveLoadService _saveLoadService;

		public LoadProject(IGameStateMachine gameStateMachine) : base(
			gameStateMachine)
		{ }

		public string FirstScene { get; set; }

		public void Enter()
		{
			_bombData.Init();
			_enemiesAtLevelsData.Init();
			_saveLoadService.Load();
			_gameStateMachine.EnterToLoadScene(FirstScene);
		}

		public void Exit()
		{ }
	}
}