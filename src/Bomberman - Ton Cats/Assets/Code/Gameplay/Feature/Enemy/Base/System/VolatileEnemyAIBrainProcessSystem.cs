﻿using Common.Component;
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
	public sealed class VolatileEnemyAIBrainProcessSystem : IEcsInitSystem,
		IEcsRunSystem, IAIAgent
	{
		[Inject] Patrolling _patrolling;
		
		[Inject] EntityWrapper _entity;
		public EntityWrapper Entity => _entity;

		EnemyAIStateMachine _stateMachine;
		
		IBehaviourTreeNode _tree;
		IBehaviourTreeNode _patrolTree;

		readonly EcsFilterInject<
				Inc<EnemyAIBlackboardComponent, Volatile, NavMeshAgentComponent>>
			_enemyFilter;

		public void Init(IEcsSystems systems)
		{
			_patrolling.Init(this);

			var builder = new BehaviourTreeBuilder();
			_patrolTree = builder
				.Sequence("Patrolling")
					.Selector("Select patrol point.")
						.Condition(_patrolling.HasCurrentPatrolPoint)
						.Do(_patrolling.SelectVolatilePatrolDestination)
					.End()
					.Selector("Process an arrival on the point.")
						.Sequence()
							.Condition(_patrolling.IsPatrolPointArrived)
							.Do(_patrolling.SelectVolatilePatrolDestination)
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
				_entity.SetEntity(enemy);
				_tree.Process();
			}
		}
	}
}