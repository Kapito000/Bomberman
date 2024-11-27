using Infrastructure.TimeService;
using Zenject;

namespace Infrastructure.GameStatus.State
{
	public sealed class GamePause : State, IState
	{
		[Inject] ITimeService _timeService;

		public GamePause(IGameStateMachine gameStateMachine) :
			base(gameStateMachine)
		{ }

		public void Enter()
		{
			_timeService.Stop();
		}

		public void Exit()
		{
			_timeService.Run();
		}
	}
}