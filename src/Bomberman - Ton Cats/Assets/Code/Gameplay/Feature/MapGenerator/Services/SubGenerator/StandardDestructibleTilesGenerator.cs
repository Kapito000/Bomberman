using System.Collections.Generic;
using Gameplay.Feature.MapGenerator.StaticData;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.MapGenerator.Services.SubGenerator
{
	public sealed class StandardDestructibleTilesGenerator
	{
		readonly StandardDestructibleTilesPlacer _tilePlacer;

		public StandardDestructibleTilesGenerator(IMapData mapData)
		{
			_tilePlacer = new StandardDestructibleTilesPlacer(mapData);
		}

		public void Create(IGrid<TileType> tilesGrid)
		{
			foreach (var cell in AvailableCells(tilesGrid))
			{
				if (CanPlace())
					tilesGrid.TrySet(TileType.Destructible, cell);
			}
		}

		IEnumerable<Vector2Int> AvailableCells(IGrid<TileType> tilesGrid) =>
			tilesGrid.AllCoordinates(TileType.None);

		bool CanPlace()
		{
			var frequency = _tilePlacer.PlaceFrequency();
			var random = Random.Range(0, 1f);
			return random < frequency;
		}
	}
}