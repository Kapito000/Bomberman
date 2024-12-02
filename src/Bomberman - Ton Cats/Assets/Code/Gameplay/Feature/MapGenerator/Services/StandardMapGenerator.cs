using System.Collections.Generic;
using Gameplay.Feature.Map.MapController;
using Gameplay.Feature.MapGenerator.Services.SubGenerator;
using Gameplay.Feature.MapGenerator.StaticData;
using Gameplay.LevelData;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.MapGenerator.Services
{
	public sealed class StandardMapGenerator : IMapGenerator
	{
		Vector2Int _heroSpawnCell;

		IGrid<TileType> _tilesGrid;
		IGrid<SpawnCellType> _spawnGrid;

		readonly IMapData _mapData;
		readonly ILevelData _levelData;

		readonly StandardHeroSpawnGenerator _heroSpawnGenerator;
		readonly StandardEnemySpawnGenerator _enemySpawnGenerator;
		readonly StandardOutLineWallGenerator _outLineWallGenerator;
		readonly StandardDestructibleTilesGenerator _destructibleTilesGenerator;
		readonly StandardIndestructibleTilesGenerator _indestructibleTilesGenerator;

		public StandardMapGenerator(IMapData mapData, ILevelData levelData)
		{
			_mapData = mapData;
			_levelData = levelData;
			_heroSpawnGenerator = new StandardHeroSpawnGenerator();
			_enemySpawnGenerator = new StandardEnemySpawnGenerator(mapData);
			_outLineWallGenerator = new StandardOutLineWallGenerator();
			_destructibleTilesGenerator = new StandardDestructibleTilesGenerator(mapData);
			_indestructibleTilesGenerator = new StandardIndestructibleTilesGenerator();
		}

		public void CreateMap()
		{
			var size = _mapData.MapSize + new Vector2Int(2, 2);
			_tilesGrid = new TilesGrid(size.x, size.y);
			_spawnGrid = new SpawnGrid(size.x, size.y);
			MapController().SetGrids(_tilesGrid, _spawnGrid);
		}

		public void CreateGroundTiles()
		{
			foreach (var pos in _tilesGrid)
				MapController().TrySet(TileType.Ground, pos);
		}

		public void CreateIndestructibleTiles()
		{
			CreateWallOutLine();
			CreateIndestructibleWalls();
			var indestructibles = _tilesGrid
				.AllCoordinates(TileType.Indestructible);
			foreach (var pos in indestructibles)
				MapController().TrySet(TileType.Indestructible, pos);
		}

		public Vector2Int CreateHeroSpawnCell()
		{
			_heroSpawnCell = _heroSpawnGenerator
				.CreateHeroSpawnPoint(_tilesGrid, _spawnGrid);
			if (MapController().TrySetHeroSpawnPoint(_heroSpawnCell) == false)
				CastCannotModifyMapMessage();
			return _heroSpawnCell;
		}

		public void CreateHeroSafeArea()
		{
			_heroSpawnGenerator.CreateSafeArea(_tilesGrid, _heroSpawnCell);
			foreach (var pos in _tilesGrid.AllCoordinates(TileType.Free))
				MapController().TrySet(TileType.Free, pos);
		}

		public IEnumerable<Vector2Int> EnemySpawnCells() =>
			_enemySpawnGenerator
				.CreateSpawnCells(_heroSpawnCell, _tilesGrid, _spawnGrid);

		public void CreateEnemySafeArea() =>
			_enemySpawnGenerator.CreateSafeArea(_tilesGrid, _spawnGrid);

		public void CreateDestructibleWalls()
		{
			_destructibleTilesGenerator.Create(_tilesGrid);
			var destructibles = MapController().AllCoordinates(TileType.Destructible);
			foreach (var cell in destructibles)
				MapController().TrySet(TileType.Destructible, cell);
		}

		public void SetNoneAsFree()
		{
			var noneCells = _tilesGrid.AllCoordinates(TileType.None);
			foreach (Vector2Int noneCell in noneCells)
				MapController().TrySet(TileType.Free, noneCell);
		}

		void CreateWallOutLine() =>
			_outLineWallGenerator.Create(_tilesGrid);

		void CreateIndestructibleWalls() =>
			_indestructibleTilesGenerator.Create(_tilesGrid);

		IMapController MapController() =>
			_levelData.MapController;

		void CastCannotModifyMapMessage() =>
			Debug.LogWarning("Cannot to modify map.");
	}
}