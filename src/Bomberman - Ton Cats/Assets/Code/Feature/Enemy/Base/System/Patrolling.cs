using System.Collections.Generic;
using System.Linq;
using AI;
using Feature.Enemy.Base.Component;
using Feature.Enemy.Base.StaticData;
using FluentBehaviourTree;
using Infrastructure.ECS;
using UnityEngine;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace Feature.Enemy.Base.System
{
	public sealed class Patrolling
	{
		[Inject] IAIData _aiData;

		readonly IAIAgent _agent;

		public Patrolling(IAIAgent agent)
		{
			_agent = agent;
		}

		public bool HasPatrolPoints()
		{
			if (Agent().Has<PatrolPoints>() == false)
				return false;

			var patrolPoints = Agent().PatrolPoints();
			if (patrolPoints == null || patrolPoints.Count == 0)
				return false;

			return true;
		}

		public bool HasCurrentPatrolPoint()
		{
			if (Agent().Has<CurrentPatrolPoint>() == false)
				return false;

			var currentPatrolPoint = Agent().CurrentPatrolPoint();
			if (currentPatrolPoint == null)
				return false;

			return true;
		}

		public bool IsPatrolPointArrived()
		{
			Vector2 pos = Agent().Transform().position;
			Vector2 pointPos = Agent().CurrentPatrolPoint().position;
			var distance = Vector2.Distance(pos, pointPos);
			if (distance < _aiData.ArrivedDestinationDistance)
				return true;
			return false;
		}

		EntityWrapper Agent() =>
			_agent.Entity;

		public BehaviourTreeStatus SelectPatrolPoint()
		{
			var patrolPoints = Agent().PatrolPoints();
			if (patrolPoints.Count == 1)
			{
				Agent().ReplaceCurrentPatrolPoint(patrolPoints[0]);
				return BehaviourTreeStatus.Success;
			}

			var points = new List<Transform>(patrolPoints);
			if (Agent().Has<CurrentPatrolPoint>())
			{
				var currentPatrolPoint = Agent().CurrentPatrolPoint();
				points.Remove(currentPatrolPoint);
			}

			var index = Random.Range(0, points.Count);
			Agent().ReplaceCurrentPatrolPoint(points[index]);
			
			return BehaviourTreeStatus.Success;
		}
	}
}