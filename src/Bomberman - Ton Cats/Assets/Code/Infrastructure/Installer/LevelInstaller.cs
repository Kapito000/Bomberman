using Factory.EntityBehaviourFactory;
using Factory.Kit;
using Factory.SystemFactory;
using Feature;
using Feature.Bomb.Factory;
using Feature.Camera.Factory;
using Feature.Explosion.Factory;
using Feature.Hero.Factory;
using Feature.HUD.Factory;
using Feature.UI.Factory;
using Gameplay.Collisions;
using Infrastructure.Boot;
using Infrastructure.ECS;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using InstantiateService;
using Leopotam.EcsLite;
using LevelData;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Infrastructure.Installer
{
	public class LevelInstaller : MonoInstaller, IInitializable
	{
		[SerializeField] Tilemap _mainTailMap;
		[SerializeField] EcsRunner _ecsRunner;

		[Inject] ILevelData _levelData;
		[Inject] IGameStateMachine _gameStateMachine;

		public override void InstallBindings()
		{
			BindInitializable();
			BindWorld();
			BindTileMap();
			BindFactories();
			BindFactoryKit();
			BindUiFactories();
			BindInstantiator();
			BindSystemFactory();
			BindDevSceneRunner();
			BindCollisionRegistry();
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

		void BindCollisionRegistry()
		{
			Container.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle();
		}

		void BindTileMap()
		{
			Container.BindInstance(_mainTailMap).AsSingle();
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
	}
}