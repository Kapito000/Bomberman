using System;
using System.Linq;
using Gameplay.Audio;
using Gameplay.Audio.MixerProvider;
using Gameplay.Audio.Service;
using Gameplay.Collisions;
using Gameplay.Feature.Enemy.Base.StaticData;
using Gameplay.Feature.Hero.StaticData;
using Gameplay.Feature.MapGenerator.Services;
using Gameplay.Feature.MapGenerator.StaticData;
using Gameplay.Feature.Timer.StaticData;
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
using UnityEngine.Audio;
using UnityEngine.Serialization;
using Zenject;

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
		[SerializeField] DirectLinkProvider _assetProvider;
		[FormerlySerializedAs("_audioMixerGroupGroupProvider"), FormerlySerializedAs("_audioMixerProvider"), SerializeField]
		AudioMixerGroupProvider _audioMixerGroupProvider;

		public override void InstallBindings()
		{
			GameTimerData();
			BindLevelData();
			BindStaticData();
			BindFactoryKit();
			BindTimeService();
			BindSceneLoader();
			BindUIFactories();
			BindAudioService();
			BindInputService();
			BindMapGenerator();
			BindEntityWrapper();
			BindAssetProvider();
			BindPhysicsService();
			BindGameStateMachine();
			BindCollisionRegistry();
			BindAudioMixerProvider();
			BindInstantiateService();
			BindEntityBehaviourFactory();
		}

		void BindAudioMixerProvider()
		{
			Container.Bind<IAudioMixerGroupProvider>()
				.FromMethod(CreateAudioMixerGroupProvider).AsSingle();
		}

		void BindAudioService()
		{
			Container.Bind<IAudioService>().To<AudioService>().AsSingle();
		}

		void GameTimerData()
		{
			Container.Bind<IGameTimerData>().FromInstance(_gameTimerData).AsSingle();
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

		IAudioMixerGroupProvider CreateAudioMixerGroupProvider()
		{
			CheckAudioMixerProviderContent();
			return _audioMixerGroupProvider;
		}

		void CheckAudioMixerProviderContent()
		{
			if (_audioMixerGroupProvider.Mixer == null)
			{
				Debug.LogWarning($"{nameof(IAudioMixerGroupProvider)} not contains " +
					$"{nameof(AudioMixer)}");
			}
			var values = Enum
				.GetValues(typeof(MixerGroup))
				.Cast<MixerGroup>()
				.ToArray();
			for (int i = 1; i < values.Length; i++)
			{
				if (_audioMixerGroupProvider.Has(values[i]) == false)
				{
					Debug.LogWarning($"{nameof(IAudioMixerGroupProvider)} not contains " +
						$"\"{values[i]}\" group.");
				}
			}
		}
	}
}