using Gameplay.Feature.MainMenu;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using Zenject;

namespace Infrastructure.Installer
{
	public sealed class MainMenuInstaller : MonoInstaller, IInitializable
	{
		[Inject] IGameStateMachine _gameStateMachine;

		public override void InstallBindings()
		{
			BindInitializable();
			BindUIServices();
		}

		public void Initialize()
		{
			_gameStateMachine.Enter<MainMenu>();
		}

		void BindUIServices()
		{
			Container.Bind<IMainMenuService>().To<MainMenuService>().AsSingle();
		}

		void BindInitializable()
		{
			Container.Bind<IInitializable>().FromInstance(this).AsSingle();
		}
	}
}