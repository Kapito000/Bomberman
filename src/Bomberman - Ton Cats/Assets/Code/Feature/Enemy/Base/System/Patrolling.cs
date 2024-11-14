using AI;
using Feature.Enemy.AI;
using Feature.Enemy.Base.Component;
using Feature.Enemy.Base.StaticData;
using FluentBehaviourTree;
using Infrastructure.ECS;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Feature.Enemy.Base.System
{
	public sealed class Patrolling
	{
		[Inject] IAIData _aiData;
		[Inject] Tilemap _tileMap;
		[Inject] FindPatrolPoints _findPatrolPoints;

		IAIAgent _agent;
		
		public void Init(IAIAgent agent)
		{
			_agent = agent;
		}

		public bool HasCurrentPatrolPoint()
		{
			return Agent().Has<CurrentDestination>();
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
			Agent().ReplaceCurrentDestination(destination);
			Agent().NavMeshAgent().SetDestination(destination);
			return BehaviourTreeStatus.Success;
		}

		EntityWrapper Agent() =>
			_agent.Entity;
	}
}