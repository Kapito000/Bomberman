using UnityEngine;

namespace MapTile
{
	public interface IDestructible : ITile
	{
		GameObject DestructiblePrefab { get; }
	}
}