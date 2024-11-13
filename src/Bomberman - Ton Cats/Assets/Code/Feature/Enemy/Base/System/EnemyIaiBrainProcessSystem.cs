using AI;
using Feature.Enemy.AI;
using Feature.Enemy.Base.Component;
using FluentBehaviourTree;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Enemy.Base.System
{
	public sealed class EnemyIaiBrainProcessSystem : IEcsInitSystem,
		IEcsRunSystem, IAIAgent
	{
		[Inject] public EntityWrapper Entity { get; private set; }

		IEnemyAIStateMachine _stateMachine;

		IBehaviourTreeNode _tree;

		readonly EcsFilterInject<Inc<BaseEnemyAIBlackboardComponent>> _enemyFilter;

		public void Init(IEcsSystems systems)
		{
			var patrolling = new Patrolling(this);

			_tree = new BehaviourTreeBuilder()
				.Sequence("Patrolling")
					.Condition(patrolling.HasPatrolPoints)
					.Selector("Select patrol point.")
						.Condition(patrolling.HasCurrentPatrolPoint)
						.Do(patrolling.SelectPatrolPoint)
					.End()
				
				
					.Sequence("Assign current movement point.")
						
						.Condition(patrolling.IsPatrolPointArrived)
					.End()
				.End()
				.Build();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var enemy in _enemyFilter.Value)
			{
				Entity.SetEntity(enemy);
				_tree.Process();
			}
		}
	}
}