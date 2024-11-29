using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Map
{
	public interface IGrid : IEnumerable<Cell>, IEnumerable<Vector2Int>
	{
		Vector2Int Size { get; }
		ref Cell this[int x, int y] { get; }
		ref Cell this[Vector2Int pos] { get; }
		bool Has(int x, int y);
		bool Has(Vector2Int pos);
	}
}