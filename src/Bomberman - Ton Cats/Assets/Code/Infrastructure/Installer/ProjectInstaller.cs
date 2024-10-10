﻿using Factory.CameraFactory;
using Factory.HeroFactory;
using Infrastructure.AssetProvider;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using Infrastructure.SceneLoader;
using InstantiateService;
using LevelData;
using StaticData.SceneNames;
using UnityEngine;
using Zenject;
using IInstantiator = InstantiateService.IInstantiator;

namespace Infrastructure.Installer
{
	public class ProjectInstaller : MonoInstaller
	{
		[SerializeField] SceneNamesData _sceneNamesData;
		[SerializeField] DirectLinkProvider _assetProvider;

		public override void InstallBindings()
		{
			BindFactories();
			BindLevelData();
			BindStaticData();
			BindSceneLoader();
			BindInstantiator();
			BindAssetProvider();
			BindGameStateMachine();
		}

		void BindInstantiator()
		{
			Container.Bind<IInstantiator>().To<StandardInstantiator>().AsSingle();
		}

		void BindFactories()
		{
			Container.Bind<ICameraFactory>().To<CameraFactory>().AsSingle();
			Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
		}

		void BindAssetProvider()
		{
			Container.Bind<IAssetProvider>().FromInstance(_assetProvider).AsSingle();
		}

		void BindLevelData()
		{
			Container.Bind<ILevelData>().To<StandardLevelData>().AsSingle();
		}

		void BindSceneLoader()
		{
			Container.Bind<ISceneLoader>().To<StandardLoader>().AsSingle();
		}

		void BindStaticData()
		{
			Container.Bind<ISceneNameData>().FromInstance(_sceneNamesData)
				.AsSingle();
		}

		void BindGameStateMachine()
		{
			Container.Bind<Game>().FromNew().AsSingle();

			Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
			Container.Bind<IState>().To<LoadGame>().AsSingle();
			Container.Bind<IState>().To<LoadScene>().AsSingle();
			Container.Bind<IState>().To<LaunchGame>().AsSingle();
			Container.Bind<IState>().To<GameLoop>().AsSingle();
			Container.Bind<IState>().To<GameExit>().AsSingle();
		}
	}
}