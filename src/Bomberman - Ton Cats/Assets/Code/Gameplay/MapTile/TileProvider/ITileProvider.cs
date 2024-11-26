using System.Collections.Generic;
using Gameplay.Map;
using Infrastructure;
using UnityEngine.Tilemaps;

namespace Gameplay.MapTile.TileProvider
{
	public interface ITileProvider : IService
	{
		IReadOnlyDictionary<CellType, TileBase> Tiles { get; }
		TileBase this[CellType type] { get; }
		bool Has(CellType type);
	}
}