using System.Collections.Generic;
using Gameplay.Feature.MapGenerator.Services.DestructibleTilesPlacer;
using Gameplay.Feature.MapGenerator.StaticData;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.MapGenerator.Services.DestructibleTilesGenerator
{
	public sealed class StandardDestructibleTilesGenerator : IDestructibleTilesGenerator
	{
		readonly IDestructibleTilesPlacer _tilePlacer;

		public StandardDestructibleTilesGenerator(IMapData mapData)
		{
			_tilePlacer = new StandardDestructibleTilesPlacer(mapData);
		}

		public void Create(IMap map)
		{
			foreach (var cell in AvailableCells(map))
			{
				if (CanPlace())
					map.TrySetCell(CellType.Destructible, cell);
			}
		}

		IEnumerable<Vector2Int> AvailableCells(IMap map) =>
			map.AllCoordinates(CellType.None);

		bool CanPlace()
		{
			var frequency = _tilePlacer.PlaceFrequency();
			var random = Random.Range(0, 1f);
			return random < frequency;
		}
	}
}