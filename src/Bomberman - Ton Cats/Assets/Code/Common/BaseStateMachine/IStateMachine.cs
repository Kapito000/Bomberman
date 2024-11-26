namespace Common.BaseStateMachine
{
	public interface IStateMachine<TKey, TState> where TState : IState
	{
		void Enter(TKey newState);
	}
}