using Gameplay.Collisions;
using Gameplay.Feature.Enemy.Base.StaticData;
using Gameplay.Feature.Hero.StaticData;
using Gameplay.Feature.MapGenerator.Services;
using Gameplay.Feature.MapGenerator.StaticData;
using Gameplay.Input;
using Gameplay.Input.Character;
using Gameplay.LevelData;
using Gameplay.MapTile.TileProvider;
using Gameplay.Physics;
using Gameplay.StaticData.SceneNames;
using Gameplay.Windows.Factory;
using Infrastructure.AssetProvider;
using Infrastructure.ECS;
using Infrastructure.Factory.EntityBehaviourFactory;
using Infrastructure.Factory.Kit;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using Infrastructure.InstantiateService;
using Infrastructure.SceneLoader;
using Infrastructure.TimeService;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
	public class ProjectInstaller : MonoInstaller
	{
		[SerializeField] AIData _aiData;
		[SerializeField] MapData _mapData;
		[SerializeField] HeroData _heroData;
		[SerializeField] SceneNamesData _sceneNamesData;
		[SerializeField] TileCollection _tileCollection;
		[SerializeField] DirectLinkProvider _assetProvider;

		public override void InstallBindings()
		{
			BindLevelData();
			BindStaticData();
			BindFactoryKit();
			BindTimeService();
			BindSceneLoader();
			BindUIFactories();
			BindInputService();
			BindMapGenerator();
			BindEntityWrapper();
			BindAssetProvider();
			BindPhysicsService();
			BindGameStateMachine();
			BindCollisionRegistry();
			BindInstantiateService();
			BindEntityBehaviourFactory();
		}

		void BindEntityBehaviourFactory()
		{
			Container.Bind<IEntityBehaviourFactory>().To<EntityBehaviourFactory>()
				.AsSingle();
		}

		void BindCollisionRegistry()
		{
			Container.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle();
		}

		void BindPhysicsService()
		{
			Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
		}

		void BindMapGenerator()
		{
			Container.Bind<IMapGenerator>().To<StandardMapGenerator>().AsSingle();
		}

		void BindTimeService()
		{
			Container.Bind<ITimeService>().To<StandardTimeService>().AsSingle();
		}

		void BindUIFactories()
		{
			Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
		}

		void BindEntityWrapper()
		{
			Container.Bind<EntityWrapper>().AsTransient().MoveIntoAllSubContainers();
		}

		void BindInputService()
		{
			Container.Bind<Controls>().AsSingle();
			Container.Bind<IInput>().To<CommonInput>().AsSingle();
			Container.Bind<ICharacterInput>().To<CharacterInputService>().AsSingle();
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
			Container.Bind<IHeroData>().FromInstance(_heroData).AsSingle();
			Container.Bind<ISceneNameData>().FromInstance(_sceneNamesData)
				.AsSingle();
			Container.Bind<IAIData>().FromInstance(_aiData).AsSingle();
			Container.Bind<IMapData>().FromInstance(_mapData).AsSingle();
			Container.Bind<ITileProvider>().FromInstance(_tileCollection).AsSingle();
		}

		void BindGameStateMachine()
		{
			Container.Bind<Game>().FromNew().AsSingle();

			Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
			Container.Bind<IState>().To<LoadGame>().AsSingle();
			Container.Bind<IState>().To<LoadScene>().AsSingle();
			Container.Bind<IState>().To<MainMenu>().AsSingle();
			Container.Bind<IState>().To<LaunchGame>().AsSingle();
			Container.Bind<IState>().To<GameLoop>().AsSingle();
			Container.Bind<IState>().To<GamePause>().AsSingle();
			Container.Bind<IState>().To<GameExit>().AsSingle();
		}

		void BindInstantiateService()
		{
			Container
				.Bind(typeof(IInstantiateService), typeof(IDiContainerDependence))
				.To<StandardInstantiateService>()
				.AsSingle();
		}

		void BindFactoryKit()
		{
			Container.Bind<IFactoryKit>().To<FactoryKit>().AsSingle();
		}
	}
}