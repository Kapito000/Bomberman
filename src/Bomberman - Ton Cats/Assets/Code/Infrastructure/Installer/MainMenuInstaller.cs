using Gameplay.Feature.FeatureControl;
using Gameplay.Feature.MainMenu.Factory;
using Gameplay.LevelData;
using Gameplay.MainMenu;
using Gameplay.UI.StaticData;
using Infrastructure.Boot;
using Infrastructure.ECS;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
	public sealed class MainMenuInstaller : MonoInstaller, IInitializable
	{
		[SerializeField] WindowKitId _windowKitId = WindowKitId.MainMenu;

		[Inject] ILevelData _levelData;
		[Inject] IGameStateMachine _gameStateMachine;

		public override void InstallBindings()
		{
			BindInitializable();
			BindFactories();
			BindUIServices();
			BindWindowKitId();
			BindDevSceneRunner();
			BindFeatureController();
		}

		public void Initialize()
		{
			InitLevelData();
			Util.ResolveDiContainerDependences(Container);
			_gameStateMachine.Enter<MainMenu>();
		}

		void InitLevelData()
		{
			_levelData.EcsRunner = Container.Resolve<IEcsRunner>();
			_levelData.DevSceneRunner = Container.Resolve<IDevSceneRunner>();
		}

		void BindWindowKitId()
		{
			Container.BindInstance(_windowKitId).AsSingle();
		}

		void BindFactories()
		{
			Container.Bind<IMainMenuFactory>().To<MainMenuFactory>().AsSingle();
		}

		void BindFeatureController()
		{
			Container.Bind<IFeatureController>().To<MainMenuFeatureController>()
				.AsSingle();
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