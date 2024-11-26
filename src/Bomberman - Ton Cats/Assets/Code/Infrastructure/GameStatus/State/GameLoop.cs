using Gameplay.Input.Character;
using Zenject;

namespace Infrastructure.GameStatus.State
{
	public sealed class GameLoop : State, IState
	{
		[Inject] ICharacterInput _characterInput;

		public GameLoop(IGameStateMachine gameStateMachine) :
			base(gameStateMachine)
		{ }

		public void Enter()
		{
			_characterInput.Enable();
		}

		public void Exit()
		{
			_characterInput.Disable();
		}
	}
}