using Infrastructure.ECS;
using UnityEngine;

namespace Extensions
{
	public static class GameObjectExtension
	{
		public static bool TyrGetEntityBehaviour(this GameObject obj,
			out EntityBehaviour entityBehaviour) =>
			obj.TryGetComponent(out entityBehaviour);
	}
}