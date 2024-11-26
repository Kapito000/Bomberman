namespace Common.BaseStateMachine
{
	public interface IState
	{
		void Enter();
		void Exit();
	}
}