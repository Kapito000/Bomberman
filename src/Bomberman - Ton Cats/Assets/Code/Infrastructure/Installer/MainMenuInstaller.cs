using Gameplay.LevelData;
using Gameplay.MainMenu;
using Infrastructure.Boot;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using Zenject;

namespace Infrastructure.Installer
{
	public sealed class MainMenuInstaller : MonoInstaller, IInitializable
	{
		[Inject] ILevelData _levelData;
		[Inject] IGameStateMachine _gameStateMachine;

		public override void InstallBindings()
		{
			BindInitializable();
			BindUIServices();
			BindDevSceneRunner();
		}

		public void Initialize()
		{
			InitLevelData();
			_gameStateMachine.Enter<MainMenu>();
		}
		
		void InitLevelData()
		{
			_levelData.DevSceneRunner = Container.Resolve<IDevSceneRunner>();
		}
		
		void BindDevSceneRunner()
		{
			Container.Bind<IDevSceneRunner>().FromComponentInHierarchy().AsSingle();
		}

		void BindUIServices()
		{
			Container.Bind<IMainMenuService>().To<MainMenuService>().AsSingle();
		}

		void BindInitializable()
		{
			Container.Bind<IInitializable>().FromInstance(this).AsSingle();
		}
	}
}