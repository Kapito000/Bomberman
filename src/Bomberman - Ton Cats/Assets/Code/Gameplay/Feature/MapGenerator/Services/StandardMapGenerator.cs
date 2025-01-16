using Gameplay.Difficult;
using Gameplay.Feature.Map.MapController;
using Gameplay.Feature.MapGenerator.Services.SubGenerator;
using Gameplay.Feature.MapGenerator.StaticData;
using Gameplay.LevelData;
using Gameplay.Map;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.MapGenerator.Services
{
	public sealed class StandardMapGenerator : IMapGenerator
	{
		Vector2Int _heroSpawnCell;

		IGrid<string> _enemySpawnGrid;
		IGrid<string> _bonusesGrid;
		IGrid<MapItem> _itemGrid;
		IGrid<TileType> _tilesGrid;

		readonly IMapData _mapData;
		readonly IGameLevelData _levelData;

		[Inject] readonly BonusGenerator _bonusGenerator;
		readonly StandardHeroSpawnGenerator _heroSpawnGenerator;
		readonly StandardEnemySpawnGenerator _enemySpawnGenerator;
		readonly StandardOutLineWallGenerator _outLineWallGenerator;
		readonly StandardDestructibleTilesGenerator _destructibleTilesGenerator;
		readonly StandardIndestructibleTilesGenerator _indestructibleTilesGenerator;

		public IGrid<string> EnemySpawnGrid => _enemySpawnGrid;
		public IGrid<string> BonusesGrid => _bonusesGrid;
		public IGrid<MapItem> ItemGrid => _itemGrid;
		public IGrid<TileType> TilesGrid => _tilesGrid;

		public StandardMapGenerator(IMapData mapData, IGameLevelData levelData,
			IDifficultService difficultService)
		{
			_mapData = mapData;
			_levelData = levelData;
			_heroSpawnGenerator = new StandardHeroSpawnGenerator();
			_enemySpawnGenerator =
				new StandardEnemySpawnGenerator(mapData, difficultService);
			_outLineWallGenerator = new StandardOutLineWallGenerator();
			_destructibleTilesGenerator =
				new StandardDestructibleTilesGenerator(mapData);
			_indestructibleTilesGenerator =
				new StandardIndestructibleTilesGenerator();
		}

		public void CreateMap()
		{
			var size = _mapData.MapSize + new Vector2Int(2, 2);
			_itemGrid = new ItemGrid(size.x, size.y);
			_tilesGrid = new TilesGrid(size.x, size.y);
			_enemySpawnGrid = new ComparableGrid<string>(size.x, size.y);
			_bonusesGrid = new StringGrid(size.x, size.y);

			_enemySpawnGenerator.SetGrids(_tilesGrid, _enemySpawnGrid);
			MapController().SetGrids(_tilesGrid, _itemGrid, _bonusesGrid);
		}

		public void CreateGroundTiles()
		{
			foreach (var cell in _tilesGrid)
				MapController().SetGround(cell);
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
			_heroSpawnCell = _heroSpawnGenerator.CreateHeroSpawnPoint(_tilesGrid);
			return _heroSpawnCell;
		}

		public void CreateHeroSafeArea()
		{
			_heroSpawnGenerator.CreateSafeArea(_tilesGrid, _heroSpawnCell);
			foreach (var pos in _tilesGrid.AllCoordinates(TileType.Free))
				MapController().TrySet(TileType.Free, pos);
		}

		public void CreateEnemySpawnCells()
		{
			_enemySpawnGenerator.CreateEnemySpawnCells(_heroSpawnCell);
			_enemySpawnGenerator.CreateSafeArea();
		}

		public void CreateDestructibleWalls()
		{
			_destructibleTilesGenerator.Create(_tilesGrid);
			var destructibles = MapController().AllCoordinates(TileType.Destructible);
			foreach (var cell in destructibles)
				MapController().TrySet(TileType.Destructible, cell);
		}

		public void CreateBonuses()
		{
			_bonusGenerator.Create(_tilesGrid, _itemGrid, _bonusesGrid);
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