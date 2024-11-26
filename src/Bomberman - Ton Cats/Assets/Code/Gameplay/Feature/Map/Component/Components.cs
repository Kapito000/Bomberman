using UnityEngine;

namespace Gameplay.Feature.Map.Component
{
	public struct TilePos { public Vector2Int Value; }
	public struct DestroyedTile { }
	public struct ChangeMapRequest { }
	public struct SetFree { }
	public struct SetDestructible { }
	public struct SetIndestructible { }
}