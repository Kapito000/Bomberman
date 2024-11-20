using System;
using Common;
using Map;
using UnityEngine.Tilemaps;

namespace MapTile.TileProvider
{
	[Serializable]
	public sealed class TilesByTypeDictionary : SerializedDictionary<CellType, TileBase>
	{ }
}