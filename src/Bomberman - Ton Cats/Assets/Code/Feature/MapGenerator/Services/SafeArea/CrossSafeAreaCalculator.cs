using UnityEngine;

namespace Feature.MapGenerator.Services.SafeArea
{
	public sealed class CrossSafeAreaCalculator : ISafeAreaCalculator
	{
		public Vector2Int[] SafeArea(Vector2Int point)
		{
			var result = new Vector2Int[5];
			result[0] = point;
			result[1] = point + Vector2Int.up;
			result[2] = point + Vector2Int.down;
			result[3] = point + Vector2Int.left;
			result[4] = point + Vector2Int.right;
			return result;
		}
	}
}