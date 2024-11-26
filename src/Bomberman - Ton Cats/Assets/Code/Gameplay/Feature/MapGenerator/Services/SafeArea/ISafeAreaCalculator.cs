using UnityEngine;

namespace Gameplay.Feature.MapGenerator.Services.SafeArea
{
	public interface ISafeAreaCalculator
	{
		Vector2Int[] SafeArea(Vector2Int point);
	}
}