using System.Collections.Generic;
using Gameplay.Map;
using Infrastructure;
using UnityEngine.Tilemaps;

namespace Gameplay.MapTile.TileProvider
{
	public interface ITileProvider : IService
	{
		IReadOnlyDictionary<TileType, TileBase> Tiles { get; }
		TileBase this[TileType type] { get; }
		bool Has(TileType type);
	}
}