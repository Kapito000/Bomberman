using Common.FluentBehaviourTree;
using Gameplay.AI;
using Gameplay.Feature.Enemy.AI;
using Gameplay.Feature.Enemy.Base.Component;
using Gameplay.Feature.Enemy.Base.StaticData;
using Gameplay.Navigation.Component;
using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Enemy.Base.System
{
	public sealed class Patrolling
	{
		[Inject] IAIData _aiData;
		[Inject] FindPatrolPoints _findPatrolPoints;
		[Inject] FindPatrolVolatilePoints _findPatrolVolatilePoints;

		IAIAgent _agent;

		public void Init(IAIAgent agent)
		{
			_agent = agent;
		}

		public bool HasCurrentPatrolPoint()
		{
			return Agent().Has<PatrolPoint>();
		}

		public bool IsPatrolPointArrived()
		{
			Vector2 pos = Agent().Transform().position;
			Vector2 pointPos = Agent().NavMeshAgent().destination;
			var distance = Vector2.Distance(pos, pointPos);
			if (distance < _aiData.ArrivedDestinationDistance)
				return true;
			return false;
		}

		public BehaviourTreeStatus SelectPatrolDestination()
		{
			var pos = Agent().TransformPos();
			var patrolDistance = _aiData.PatrolDistance;
			var destination = _findPatrolPoints.CalculatePoint(pos, patrolDistance);
			Agent().ReplacePatrolPoint(destination);
			Agent().ReplaceAgentDestination(destination);
			return BehaviourTreeStatus.Success;
		}

		public BehaviourTreeStatus SelectVolatilePatrolDestination()
		{
			var pos = Agent().TransformPos();
			var patrolDistance = _aiData.PatrolDistance;
			var destination = _findPatrolVolatilePoints
				.CalculatePoint(pos, patrolDistance);
			Agent().ReplacePatrolPoint(destination);
			Agent().ReplaceAgentDestination(destination);
			return BehaviourTreeStatus.Success;
		}

		EntityWrapper Agent() =>
			_agent.Entity;
	}
}