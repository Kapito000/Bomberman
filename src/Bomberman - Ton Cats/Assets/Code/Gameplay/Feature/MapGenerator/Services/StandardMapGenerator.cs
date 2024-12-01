using Gameplay.Feature.MapGenerator.Services.SubGenerator;
using Gameplay.Feature.MapGenerator.StaticData;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.MapGenerator.Services
{
	public sealed class StandardMapGenerator : IMapGenerator
	{
		IMapData _mapData;

		StandardHeroSpawnGenerator _heroSpawnGenerator;
		StandardEnemySpawnGenerator _enemySpawnGenerator;
		StandardOutLineWallGenerator _outLineWallGenerator;
		StandardDestructibleTilesGenerator _destructibleTilesGenerator;
		StandardIndestructibleTilesGenerator _indestructibleTilesGenerator;

		Vector2Int _heroSpawnPoint;

		public IGrid<TileType> TilesGrid { get; private set; }
		public IGrid<SpawnCellType> SpawnGrid { get; private set; }

		public StandardMapGenerator(IMapData mapData)
		{
			_mapData = mapData;
			_heroSpawnGenerator = new StandardHeroSpawnGenerator();
			_enemySpawnGenerator = new StandardEnemySpawnGenerator(mapData);
			_outLineWallGenerator = new StandardOutLineWallGenerator();
			_destructibleTilesGenerator = new StandardDestructibleTilesGenerator(mapData);
			_indestructibleTilesGenerator = new StandardIndestructibleTilesGenerator();
		}

		public void CreateMap()
		{
			var size = _mapData.MapSize + new Vector2Int(2, 2);
			TilesGrid = new TilesGrid(size.x, size.y);
			CreateWallOutLine();
			CreateIndestructibleWalls();
			CreatePlayerSpawnArea();
			CreateEnemiesSpawnPoints();
			CreateDestructibleWalls();
			SetNoneAsFree();
		}

		void CreateWallOutLine() =>
			_outLineWallGenerator.Create(TilesGrid);

		void CreateIndestructibleWalls() =>
			_indestructibleTilesGenerator.Create(TilesGrid);

		void CreatePlayerSpawnArea()
		{
			_heroSpawnPoint = _heroSpawnGenerator
				.CreateHeroSpawnPoint(TilesGrid, SpawnGrid);
			_heroSpawnGenerator.CreateSafeArea(TilesGrid, _heroSpawnPoint);
		}

		void CreateEnemiesSpawnPoints()
		{
			_enemySpawnGenerator
				.CreateSpawnPoints(_heroSpawnPoint, TilesGrid, SpawnGrid);
			_enemySpawnGenerator.CreateSafeArea(TilesGrid, SpawnGrid);
		}

		void CreateDestructibleWalls() =>
			_destructibleTilesGenerator.Create(TilesGrid);

		void SetNoneAsFree()
		{
			var noneCells = TilesGrid.AllCoordinates(TileType.None);
			foreach (Vector2Int noneCell in noneCells)
				TilesGrid.TrySet(TileType.Free, noneCell);
		}
	}
}