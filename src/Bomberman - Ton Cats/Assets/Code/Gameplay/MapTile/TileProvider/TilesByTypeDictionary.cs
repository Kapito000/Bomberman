using System;
using Common;
using Common.Dictionary;
using Gameplay.Map;
using UnityEngine.Tilemaps;

namespace Gameplay.MapTile.TileProvider
{
	[Serializable]
	public sealed class TilesByTypeDictionary : SerializedDictionary<TileType, TileBase>
	{ }
}