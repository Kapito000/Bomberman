using Gameplay.Feature.Enemy.Base.Component;
using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay.Feature.Enemy
{
	public static class EntityWrapperExtension
	{
		public static Vector2 PatrolPoint(this EntityWrapper e)
		{
			ref var component = ref e.Get<PatrolPoint>();
			return component.Value;
		}

		public static EntityWrapper ReplacePatrolPoint(this EntityWrapper e, Vector2 value)
		{
			ref var component = ref e.ReplaceComponent<PatrolPoint>();
			component.Value = value;
			return e;
		}
	}
}