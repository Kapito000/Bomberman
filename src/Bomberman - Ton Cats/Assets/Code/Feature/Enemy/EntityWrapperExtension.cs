using Feature.Enemy.Base.Component;
using Feature.Enemy.Component;
using Infrastructure.ECS;
using UnityEngine;

namespace Feature.Enemy
{
	public static class EntityWrapperExtension
	{
		public static Vector2 CurrentPatrolPoint(this EntityWrapper e)
		{
			ref var currentPatrolPoint = ref e.Get<CurrentDestination>();
			return currentPatrolPoint.Value;
		}

		public static Vector2 ReplaceCurrentDestination(this EntityWrapper e,
			Vector2 destination)
		{
			ref var currentPatrolPoint = ref e.ReplaceComponent<CurrentDestination>();
			return currentPatrolPoint.Value = destination;
		}

		public static float AttackRadius(this EntityWrapper e)
		{
			ref var attackRadius = ref e.Get<AttackRadius>();
			return attackRadius.Value;
		}
	}
}