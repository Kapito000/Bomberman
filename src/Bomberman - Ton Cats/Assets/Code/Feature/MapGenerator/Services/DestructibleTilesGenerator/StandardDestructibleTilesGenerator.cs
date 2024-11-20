using System.Collections.Generic;
using Feature.MapGenerator.Services.DestructibleTilesPlacer;
using Feature.MapGenerator.StaticData;
using Gameplay.Map;
using UnityEngine;

namespace Feature.MapGenerator.Services.DestructibleTilesGenerator
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
			var availableCells = AvailableCells(map);
			foreach (var cell in availableCells)
			{
				if (CanPlace())
					map.TrySetDestructible(cell);
			}
		}

		List<Vector2Int> AvailableCells(IMap map) =>
			map.CalculateNoneCells();

		bool CanPlace()
		{
			var frequency = _tilePlacer.PlaceFrequency();
			var random = Random.Range(0, 1f);
			return random < frequency;
		}
	}
}