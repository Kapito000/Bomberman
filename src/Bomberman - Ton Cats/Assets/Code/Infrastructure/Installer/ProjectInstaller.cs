using Windows;
using Windows.Factory;
using Feature.Hero.StaticData;
using Infrastructure.AssetProvider;
using Infrastructure.ECS;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using Infrastructure.SceneLoader;
using Input;
using Input.Character;
using LevelData;
using StaticData.Physic;
using StaticData.SceneNames;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
	public class ProjectInstaller : MonoInstaller
	{
		[SerializeField] HeroData _heroData;
		[SerializeField] PhysicsData _physicsData;
		[SerializeField] SceneNamesData _sceneNamesData;
		[SerializeField] DirectLinkProvider _assetProvider;

		public override void InstallBindings()
		{
			BindLevelData();
			BindStaticData();
			BindSceneLoader();
			BindUIFactories();
			BindUIServices();
			BindInputService();
			BindEntityWrapper();
			BindAssetProvider();
			BindGameStateMachine();
		}
		
		void BindUIServices()
		{
			Container.Bind<IWindowService>().To<WindowService>().AsSingle();
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
			Container.Bind<IPhysicsData>().FromInstance(_physicsData).AsSingle();
			Container.Bind<ISceneNameData>().FromInstance(_sceneNamesData)
				.AsSingle();
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
			Container.Bind<IState>().To<GameExit>().AsSingle();
		}
	}
}