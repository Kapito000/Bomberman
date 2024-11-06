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
		}
		
		public void Initialize()
		{
			_gameStateMachine.Enter<MainMenu>();
		}
		
		void BindInitializable()
		{
			Container.BindInterfacesTo<LevelInstaller>().FromInstance(this)
				.AsSingle();
		}
	}
}