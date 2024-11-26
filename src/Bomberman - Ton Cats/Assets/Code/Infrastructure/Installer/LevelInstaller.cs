using Gameplay.AI.Navigation;
using Gameplay.Feature;
using Gameplay.Feature.Bomb.Factory;
using Gameplay.Feature.Camera.Factory;
using Gameplay.Feature.Enemy.AI;
using Gameplay.Feature.Enemy.Base.Factory;
using Gameplay.Feature.Enemy.Base.System;
using Gameplay.Feature.Explosion.Factory;
using Gameplay.Feature.Hero.Factory;
using Gameplay.Feature.HUD.Factory;
using Gameplay.Feature.Map.MapController;
using Gameplay.Feature.UI.Factory;
using Gameplay.FinishLevel.Factory;
using Gameplay.LevelData;
using Gameplay.MapView;
using Infrastructure.Boot;
using Infrastructure.ECS;
using Infrastructure.Factory.EntityBehaviourFactory;
using Infrastructure.Factory.Kit;
using Infrastructure.Factory.SystemFactory;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using Infrastructure.InstantiateService;
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
			BindFactoryKit();
			BindUiFactories();
			BindInstantiator();
			BindAIFunctional();
			BindMapController();
			BindSystemFactory();
			BindDevSceneRunner();
			BindNavigationSurface();
			BindFeatureController();
		}

		public void Initialize()
		{
			InitLevelData();
			_gameStateMachine.Enter<LaunchGame>();
		}

		void InitLevelData()
		{
			_levelData.World = Container.Resolve<EcsWorld>();
			_levelData.EcsRunner = _ecsRunner;
			_levelData.DevSceneRunner = Container.Resolve<IDevSceneRunner>();
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