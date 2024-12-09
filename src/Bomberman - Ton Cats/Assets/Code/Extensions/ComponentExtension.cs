using Infrastructure.ECS;
using UnityEngine;

namespace Extensions
{
	public static class ComponentExtension
	{
		public static bool TyrGetEntityBehaviour(this Component component,
			out EntityBehaviour entityBehaviour) =>
			component.TryGetComponent(out entityBehaviour);
	}
}