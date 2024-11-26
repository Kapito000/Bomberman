using System;
using Common;
using Gameplay.Map;
using UnityEngine.Tilemaps;

namespace Gameplay.MapTile.TileProvider
{
	[Serializable]
	public sealed class TilesByTypeDictionary : SerializedDictionary<CellType, TileBase>
	{ }
}