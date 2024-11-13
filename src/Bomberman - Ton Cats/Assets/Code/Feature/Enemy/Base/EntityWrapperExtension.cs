using System.Collections.Generic;
using Feature.Enemy.Base.Component;
using Infrastructure.ECS;
using UnityEngine;

namespace Feature.Enemy.Base
{
	public static class EntityWrapperExtension
	{
		public static List<Transform> PatrolPoints(this EntityWrapper e)
		{
			ref var patrolPoints = ref e.Get<PatrolPoints>();
			return patrolPoints.Value;
		}

		public static EntityWrapper AddPatrolPoints(this EntityWrapper e)
		{
			ref var patrolPoints = ref e.AddComponent<PatrolPoints>();
			patrolPoints.Value = new List<Transform>();
			return e;
		}

		public static EntityWrapper AddCurrentPatrolPoint(this EntityWrapper e,
			Transform point)
		{
			ref var currentPatrolPoint = ref e.AddComponent<CurrentPatrolPoint>();
			currentPatrolPoint.Value = point;
			return e;
		}
		
		public static Transform CurrentPatrolPoint(this EntityWrapper e)
		{
			ref var currentPatrolPoint = ref e.Get<CurrentPatrolPoint>();
			return currentPatrolPoint.Value;
		}
		
		public static Transform ReplaceCurrentPatrolPoint(this EntityWrapper e,
			Transform point)
		{
			ref var currentPatrolPoint = ref e.ReplaceComponent<CurrentPatrolPoint>();
			return currentPatrolPoint.Value = point;
		}
	}
}