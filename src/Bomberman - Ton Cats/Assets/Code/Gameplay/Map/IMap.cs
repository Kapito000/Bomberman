using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Map
{
	public interface IMap
	{
		Vector2Int Size { get; }
		IReadOnlyList<Vector2Int> Indestuctibles { get; }
		bool TrySetIndestructible(int x, int y);
	}
}