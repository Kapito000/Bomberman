using UnityEngine;

namespace Extensions
{
	public static class TransformExtension
	{
		public static Vector2 Pos2(this Transform transform) => 
			new(transform.position.x, transform.position.y);
	}
}