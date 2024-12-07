﻿using Gameplay.Feature.FeatureControl;
using Gameplay.Feature.MainMenuMusic.Factory;
using Gameplay.LevelData;
using Gameplay.MainMenu;
using Infrastructure.Boot;
using Infrastructure.ECS;
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
			BindFactories();
			BindUIServices();
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

		void BindFactories()
		{
			Container.Bind<IMainMenuMusicFactory>().To<MainMenuMusicFactory>().AsSingle();
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