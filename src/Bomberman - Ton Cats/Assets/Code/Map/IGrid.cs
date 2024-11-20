using UnityEngine;

namespace Map
{
	public interface IGrid
	{
		ref Cell this[int x, int y] { get; }
		Vector2Int Size { get; }
		bool Has(int x, int y);
	}
}