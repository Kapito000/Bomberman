using Common.FluentBehaviourTree;
using Gameplay.AI;
using Gameplay.Feature.Enemy.AI;
using Gameplay.Feature.Enemy.Base.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Enemy.Base.System
{
	public sealed class EnemyAIBrainProcessSystem : IEcsInitSystem,
		IEcsRunSystem, IAIAgent
	{
		[Inject] Patrolling _patrolling;
		
		[Inject] public EntityWrapper Entity { get; private set; }

		EnemyAIStateMachine _stateMachine;
		
		IBehaviourTreeNode _tree;
		IBehaviourTreeNode _patrolTree;

		readonly EcsFilterInject<Inc<BaseEnemyAIBlackboardComponent>> _enemyFilter;

		public void Init(IEcsSystems systems)
		{
			_patrolling.Init(this);

			var builder = new BehaviourTreeBuilder();
			_patrolTree = builder
				.Sequence("Patrolling")
					.Selector("Select patrol point.")
						.Condition(_patrolling.HasCurrentPatrolPoint)
						.Do(_patrolling.SelectPatrolDestination)
					.End()
					// .Selector("Find new patrol points.")
					// .End()
					.Selector("Process an arrival on the point.")
						.Sequence()
							.Condition(_patrolling.IsPatrolPointArrived)
							.Do(_patrolling.SelectPatrolDestination)
						.End()
					.End()
				.End()
				.Build();

			_tree = builder
				.Selector()
				.Splice(_patrolTree)
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