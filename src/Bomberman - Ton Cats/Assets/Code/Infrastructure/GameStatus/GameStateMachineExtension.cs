using Infrastructure.GameStatus.State;

namespace Infrastructure.GameStatus
{
	public static class GameStateMachineExtension
	{
		public static void EnterToLoadScene(this IGameStateMachine stateMachine,
			string sceneName)
		{
			stateMachine.GetState<LoadScene>().LoadingSceneName = sceneName;
			stateMachine.Enter<LoadScene>();
		}
	}
}