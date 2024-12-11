using Gameplay.Audio.ClipProvider;
using Gameplay.Audio.Factory;
using Gameplay.Audio.MixerGroupProvider;
using Gameplay.Audio.Player;
using Gameplay.Audio.Service;
using Gameplay.Collisions;
using Gameplay.Feature.Audio.Behaviour;
using Gameplay.Feature.Enemy.Base.StaticData;
using Gameplay.Feature.GameMusic.Factory;
using Gameplay.Feature.Hero.StaticData;
using Gameplay.Feature.MapGenerator.Services;
using Gameplay.Feature.MapGenerator.StaticData;
using Gameplay.Feature.Timer.StaticData;
using Gameplay.GameSettings;
using Gameplay.GameSettings.Audio;
using Gameplay.GameSettings.StaticData;
using Gameplay.GameSettings.StaticData.Audio;
using Gameplay.Input;
using Gameplay.Input.Character;
using Gameplay.LevelData;
using Gameplay.MapTile.TileProvider;
using Gameplay.Physics;
using Gameplay.StaticData.SceneNames;
using Gameplay.UI.Factory;
using Gameplay.UI.StaticData;
using Gameplay.Windows;
using Gameplay.Windows.Factory;
using Infrastructure.AssetProvider;
using Infrastructure.ECS;
using Infrastructure.Factory.EntityBehaviourFactory;
using Infrastructure.Factory.Kit;
using Infrastructure.Factory.SystemFactory;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using Infrastructure.InstantiateService;
using Infrastructure.SceneLoader;
using Infrastructure.TimeService;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;
using AudioSettings = Gameplay.GameSettings.Audio.AudioSettings;

namespace Infrastructure.Installer
{
	public class ProjectInstaller : MonoInstaller
	{
		[SerializeField] AIData _aiData;
		[SerializeField] MapData _mapData;
		[SerializeField] HeroData _heroData;
		[SerializeField] GameTimerData _gameTimerData;
		[SerializeField] SceneNamesData _sceneNamesData;
		[SerializeField] TileCollection _tileCollection;
		[SerializeField] WindowKitData _windowKitData;
		[SerializeField] AudioClipProvider _audioClipProvider;
		[SerializeField] DirectLinkProvider _assetProvider;
		[SerializeField] AudioMixerProvider _audioMixerProvider;

		public override void InstallBindings()
		{
			BindWorld();
			BindPools();
			BindEcsRunner();
			GameTimerData();
			BindLevelData();
			BindFactories();
			BindStaticData();
			BindFactoryKit();
			BindTimeService();
			BindSceneLoader();
			BindUIFactories();
			BindAudioService();
			BindInputService();
			BindMapGenerator();
			BindMusicFactory();
			BindWindowService();
			BindSystemFactory();
			BindEntityWrapper();
			BindAssetProvider();
			BindPhysicsService();
			BindGameStateMachine();
			BindAudioClipProvider();
			BindCollisionRegistry();
			BindAudioMixerProvider();
			BindInstantiateService();
			BindGameSettingsService();
			BindEntityBehaviourFactory();
		}

		void BindGameSettingsService()
		{
			Container.Bind<IGameSettings>().To<GameSettingsService>().AsSingle();
			Container.Bind<IAudioSetting>().To<AudioSettings>().AsSingle()
				.WhenInjectedInto<IGameSettings>();
		}

		void BindPools()
		{
			BindAudioSourcePool();
		}

		void BindAudioSourcePool()
		{
			Container.BindMemoryPool<PooledAudioSource, PooledAudioSource.Pool>()
				.FromNewComponentOnNewGameObject()
				.WithGameObjectName(Constant.ObjectName.c_PooledAudioSource)
				.UnderTransformGroup(Constant.ObjectName.c_PooledAudioSourcesParent)
				.AsSingle()
				.MoveIntoAllSubContainers();
		}

		void BindFactories()
		{
			Container.Bind<IGameMusicFactory>().To<GameMusicFactory>()
				.AsSingle()
				.MoveIntoAllSubContainers();
		}

		void BindMusicFactory()
		{
			Container.Bind<IMusicFactory>().To<MusicFactory>()
				.AsSingle()
				.MoveIntoAllSubContainers();
		}

		void BindWorld()
		{
			Container.Bind<EcsWorld>().FromMethod(CreateNewWorld).AsSingle()
				.MoveIntoAllSubContainers();

			EcsWorld CreateNewWorld() => new();
		}


		void BindEcsRunner()
		{
			Container.Bind<IEcsRunner>().To<EcsRunner>()
				.FromNewComponentOnNewGameObject()
				.WithGameObjectName(Constant.ObjectName.c_EcsRunnerName)
				.UnderTransformGroup(Constant.ObjectName.c_Services)
				.AsSingle()
				.OnInstantiated<EcsRunner>((c, runner) => runner.enabled = false)
				.MoveIntoAllSubContainers();
		}

		void BindSystemFactory()
		{
			Container.Bind<ISystemFactory>().To<StandardSystemFactory>()
				.AsSingle()
				.MoveIntoAllSubContainers();
		}

		void BindAudioClipProvider()
		{
			Container.Bind<IAudioClipProvider>().FromInstance(_audioClipProvider)
				.AsSingle();
		}

		void BindAudioMixerProvider()
		{
			Container.Bind<IAudioMixerProvider>().FromInstance(_audioMixerProvider)
				.AsSingle();
		}

		void BindAudioService()
		{
			Container.Bind<IAudioService>().To<AudioService>().AsSingle();
			Container.Bind<IAudioPlayer>().To<AudioPlayer>()
				.AsSingle()
				.WhenInjectedInto<IAudioService>();
		}

		void GameTimerData()
		{
			Container.Bind<IGameTimerData>().FromInstance(_gameTimerData).AsSingle();
		}

		void BindEntityBehaviourFactory()
		{
			Container.Bind<IEntityBehaviourFactory>().To<EntityBehaviourFactory>()
				.AsSingle()
				.MoveIntoAllSubContainers();
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

		void BindWindowService()
		{
			Container.Bind<IWindowService>().To<WindowService>().AsSingle()
				.MoveIntoAllSubContainers();
		}

		void BindUIFactories()
		{
			Container.Bind<IUiFactory>().To<UiFactory>().AsSingle()
				.MoveIntoAllSubContainers();
			Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle()
				.MoveIntoAllSubContainers();
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
			Container.BindInterfacesTo<StandardLevelData>().AsSingle();
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
			Container.Bind<IWindowKitData>().FromInstance(_windowKitData).AsSingle();
			Container.Bind<IGameSettingsStartValueData>().To<GameSettingsStartValue>()
				.AsSingle();
			Container.Bind<IAudioStartValueData>().To<AudioStartValueData>()
				.AsSingle().WhenInjectedInto<IGameSettingsStartValueData>();
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
				.AsSingle()
				.MoveIntoAllSubContainers();
		}

		void BindFactoryKit()
		{
			Container.Bind<IFactoryKit>().To<FactoryKit>()
				.AsSingle()
				.MoveIntoAllSubContainers();
		}
	}
}