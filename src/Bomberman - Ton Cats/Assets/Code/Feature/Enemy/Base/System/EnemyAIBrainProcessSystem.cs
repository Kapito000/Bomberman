using AI;
using Feature.Enemy.Base.Component;
using FluentBehaviourTree;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Enemy.Base.System
{
	
	public sealed class EnemyAIBrainProcessSystem : IEcsInitSystem,
		IEcsRunSystem, IAIAgent
	{
		[Inject] Patrolling _patrolling;
		
		[Inject] public EntityWrapper Entity { get; private set; }

		IBehaviourTreeNode _tree;

		readonly EcsFilterInject<Inc<BaseEnemyAIBlackboardComponent>> _enemyFilter;

		public void Init(IEcsSystems systems)
		{
			_patrolling.Init(this);
			
			_tree = new BehaviourTreeBuilder()
				.Sequence("Patrolling")
					.Condition(_patrolling.HasPatrolPoints)
					.Selector("Select patrol point.")
						.Condition(_patrolling.HasCurrentPatrolPoint)
						.Do(_patrolling.SelectPatrolPoint)
					.End()
					.Selector()
						.Sequence()
							.Condition(_patrolling.IsPatrolPointArrived)
							.Do(_patrolling.SelectPatrolPoint)
						.End()
						.Do(_patrolling.SetDestination)
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