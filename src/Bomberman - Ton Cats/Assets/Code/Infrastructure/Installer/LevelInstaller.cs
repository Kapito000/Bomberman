﻿using Factory;
using Factory.CameraFactory;
using Factory.EntityBehaviourFactory;
using Factory.HeroFactory;
using Factory.HudFactory;
using Factory.Kit;
using Factory.SystemFactory;
using Factory.UiFactory;
using Feature;
using Infrastructure.Boot;
using Infrastructure.ECS;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using InstantiateService;
using Leopotam.EcsLite;
using LevelData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
	public class LevelInstaller : MonoInstaller, IInitializable
	{
		[SerializeField] EcsRunner _ecsRunner;

		[Inject] ILevelData _levelData;
		[Inject] IGameStateMachine _gameStateMachine;

		public override void InstallBindings()
		{
			BindInitializable();
			BindWorld();
			BindFactories();
			BindFactoryKit();
			BindUiFactories();
			BindInstantiator();
			BindSystemFactory();
			BindDevSceneRunner();
			BindFeatureController();
		}

		public void Initialize()
		{
			InitLevelData();
			_gameStateMachine.Enter<LaunchGame>();
		}

		void InitLevelData()
		{
			_levelData.EcsRunner = _ecsRunner;
			_levelData.DevSceneRunner = Container.Resolve<IDevSceneRunner>();
		}

		void BindUiFactories()
		{
			Container.Bind<IUiFactory>().To<UiFactory>().AsSingle();
			Container.Bind<IHudFactory>().To<HudFactory>().AsSingle();
		}

		void BindDevSceneRunner()
		{
			Container.Bind<IDevSceneRunner>().FromComponentInHierarchy().AsSingle();
		}

		void BindInstantiator()
		{
			Container.Bind<IInstantiateService>().To<StandardInstantiateService>()
				.AsSingle();
		}

		void BindFactoryKit()
		{
			Container.Bind<IFactoryKit>().To<FactoryKit>().AsSingle();
		}

		void BindFactories()
		{
			Container.Bind<IEntityBehaviourFactory>().To<EntityBehaviourFactory>()
				.AsSingle();
			Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
			Container.Bind<ICameraFactory>().To<CameraFactory>().AsSingle();
		}

		void BindFeatureController()
		{
			Container.Bind<FeatureController>().AsSingle();
		}

		void BindSystemFactory()
		{
			Container.Bind<ISystemFactory>().To<StandardSystemFactory>().AsSingle();
		}

		void BindWorld()
		{
			Container.Bind<EcsWorld>().FromNew().AsSingle();
		}

		void BindInitializable()
		{
			Container.BindInterfacesTo<LevelInstaller>().FromInstance(this)
				.AsSingle();
		}
	}
}