using Gameplay.AI.Navigation;
using Gameplay.Feature;
using Gameplay.Feature.Bomb.Factory;
using Gameplay.Feature.Camera.Factory;
using Gameplay.Feature.Enemy.AI;
using Gameplay.Feature.Enemy.Base.Factory;
using Gameplay.Feature.Enemy.Base.System;
using Gameplay.Feature.Explosion.Factory;
using Gameplay.Feature.FinishLevel.Factory;
using Gameplay.Feature.Hero.Factory;
using Gameplay.Feature.HUD.Factory;
using Gameplay.Feature.Map.MapController;
using Gameplay.Feature.UI.Factory;
using Gameplay.LevelData;
using Gameplay.MapView;
using Gameplay.Windows;
using Infrastructure.Boot;
using Infrastructure.ECS;
using Infrastructure.Factory.SystemFactory;
using Infrastructure.FinishLevel;
using Infrastructure.FinishLevel.Condition;
using Infrastructure.FinishLevel.Condition.GameOver;
using Infrastructure.FinishLevel.Condition.LevelComplete;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using Leopotam.EcsLite;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Infrastructure.Installer
{
	public sealed class LevelInstaller : MonoInstaller, IInitializable
	{
		[SerializeField] Tilemap _groundTailMap;
		[SerializeField] Tilemap _destructibleTailMap;
		[SerializeField] Tilemap _indestructibleTailMap;
		[SerializeField] GameEcsRunner _ecsRunner;
		[SerializeField] NavMeshSurface _navMeshSurface;

		[Inject] ILevelData _levelData;
		[Inject] IGameStateMachine _gameStateMachine;

		public override void InstallBindings()
		{
			BindInitializable();
			BindWorld();
			BindMapView();
			BindFactories();
			BindAIFunctional();
			BindMapController();
			BindSystemFactory();
			BindDevSceneRunner();
			BindWindowServices();
			BindNavigationSurface();
			BindFeatureController();
			BindFinishLevelService();
		}

		public void Initialize()
		{
			InitLevelData();
			Util.ResolveDiContainerDependences(Container);
			_gameStateMachine.Enter<LaunchGame>();
		}

		void InitLevelData()
		{
			_levelData.World = Container.Resolve<EcsWorld>();
			_levelData.EcsRunner = _ecsRunner;
			_levelData.MapController = Container.Resolve<IMapController>();
			_levelData.DevSceneRunner = Container.Resolve<IDevSceneRunner>();
		}

		void BindFinishLevelService()
		{
			Container.Bind<IFinishLevelService>().To<FinishLevelService>().AsSingle();
			Container.BindInterfacesTo<KillAllEnemies>().AsSingle();
			Container.BindInterfacesTo<GameTimerCondition>().AsSingle();
			Container.BindInterfacesTo<HeroHealthCondition>().AsSingle();
			Container.BindInterfacesTo<HeroEnteredIntoFinishLevelDoor>().AsSingle();
		}
		
		void BindWindowServices()
		{
			Container.Bind<IWindowService>().To<WindowService>().AsSingle();
		}

		void BindMapController()
		{
			Container.Bind<IMapController>().To<StandardMapController>().AsSingle();
		}

		void BindNavigationSurface()
		{
			Container.Bind<NavMeshSurface>().FromInstance(_navMeshSurface).AsSingle();
			Container.Bind<INavigationSurface>().To<AINavigationSurface>().AsSingle();
		}

		void BindMapView()
		{
			Container.Bind<IMapView>().FromMethod(CreateMapView).AsSingle();
		}

		void BindAIFunctional()
		{
			Container.Bind<Patrolling>().AsSingle();
			Container.Bind<FindPatrolPoints>().AsSingle();
		}

		void BindDevSceneRunner()
		{
			Container.Bind<IDevSceneRunner>().FromComponentInHierarchy().AsSingle();
		}

		void BindFactories()
		{
			Container.Bind<IUiFactory>().To<UiFactory>().AsSingle();
			Container.Bind<IHudFactory>().To<HudFactory>().AsSingle();
			Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
			Container.Bind<IBombFactory>().To<BombFactory>().AsSingle();
			Container.Bind<ICameraFactory>().To<CameraFactory>().AsSingle();
			Container.Bind<IExplosionFactory>().To<ExplosionFactory>().AsSingle();
			Container.Bind<IBaseEnemyFactory>().To<BaseEnemyFactory>().AsSingle();
			Container.Bind<IFinishLevelFactory>().To<FinishLevelFactory>().AsSingle();
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
			Container.Bind<EcsWorld>().FromInstance(new EcsWorld()).AsSingle();
		}

		void BindInitializable()
		{
			Container.BindInterfacesTo<LevelInstaller>().FromInstance(this)
				.AsSingle();
		}

		IMapView CreateMapView()
		{
			var gameTileMap = Container.Instantiate<MapView>(
				new[] { _groundTailMap, _destructibleTailMap, _indestructibleTailMap });
			return gameTileMap;
		}
	}
}