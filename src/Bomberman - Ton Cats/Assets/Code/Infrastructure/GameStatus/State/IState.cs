namespace Infrastructure.GameStatus.State
{
	public interface IState
	{
		void Enter();
		void Exit();
	}
}