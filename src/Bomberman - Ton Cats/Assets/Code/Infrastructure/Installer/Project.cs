using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using Infrastructure.SceneLoader;
using StaticData.SceneNames;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
	public class Project : MonoInstaller
	{
		[SerializeField] SceneNamesData _sceneNamesData;

		public override void InstallBindings()
		{
			BindStaticData();
			BindSceneLoader();
			BindGameStateMachine();
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
			Container.Bind<IState>().To<GameLoop>().AsSingle();
			Container.Bind<IState>().To<GameExit>().AsSingle();
		}
	}
}