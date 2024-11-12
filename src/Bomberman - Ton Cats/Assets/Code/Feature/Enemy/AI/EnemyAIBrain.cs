namespace Feature.Enemy.AI
{
	public sealed class EnemyAIBrain
	{
		IEnemyAIStateMachine _stateMachine;
		IEnemyDecisionMakingBlock _decisionMakingBlock;

		public EnemyAIBrain()
		{
			_stateMachine = new EnemyAIStateMachine();
			_decisionMakingBlock = new EnemyDecisionMakingBlock();
		}
	}
}