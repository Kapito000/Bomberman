using Gameplay.Feature.MapGenerator.Services.DestructibleTilesGenerator;
using Gameplay.Feature.MapGenerator.Services.EnemySpawnGenerator;
using Gameplay.Feature.MapGenerator.Services.HeroSpawnGenerator;
using Gameplay.Feature.MapGenerator.Services.IndestructibleWallsGenerator;
using Gameplay.Feature.MapGenerator.Services.OutLineWallGenerator;
using Gameplay.Feature.MapGenerator.StaticData;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.MapGenerator.Services
{
	public sealed class StandardMapGenerator : IMapGenerator
	{
		IMapData _mapData;

		IHeroSpawnGenerator _heroSpawnGenerator;
		IEnemySpawnGenerator _enemySpawnGenerator;
		IOutLineWallGenerator _outLineWallGenerator;
		IDestructibleTilesGenerator _destructibleTilesGenerator;
		IIndestructibleTilesGenerator _indestructibleTilesGenerator;

		public IMap Map { get; private set; }

		public StandardMapGenerator(IMapData mapData)
		{
			_mapData = mapData;
			_heroSpawnGenerator = new StandardHeroSpawnGenerator();
			_enemySpawnGenerator = new StandardEnemySpawnGenerator(mapData);
			_outLineWallGenerator = new StandardOutLineWallGenerator();
			_destructibleTilesGenerator = new StandardDestructibleTilesGenerator(mapData);
			_indestructibleTilesGenerator = new StandardIndestructibleTilesGenerator();
		}

		public IMap CreateMap()
		{
			var size = _mapData.MapSize + new Vector2Int(2, 2);
			Map = new StandardTileMap(new TileGrid(size.x, size.y));
			CreateWallOutLine(Map);
			CreateIndestructibleWalls(Map);
			CreatePlayerSpawnArea(Map);
			CreateEnemies(Map);
			CreateDestructibleWalls(Map);
			SetNoneAsFree(Map);
			return Map;
		}

		public IMap CleanMap()
		{
			var size = Map.Size;
			return new StandardTileMap(new TileGrid(size.x, size.y));
		}

		void CreateWallOutLine(IMap map) =>
			_outLineWallGenerator.Create(map);

		void CreateIndestructibleWalls(IMap map) =>
			_indestructibleTilesGenerator.Create(map);

		void CreatePlayerSpawnArea(IMap map) =>
			_heroSpawnGenerator.CreateSpawnArea(map);

		void CreateEnemies(IMap map) =>
			_enemySpawnGenerator.CreateSpawnArea(map);

		void CreateDestructibleWalls(IMap map) =>
			_destructibleTilesGenerator.Create(map);

		void SetNoneAsFree(IMap map)
		{
			var noneCells = map.CalculateNoneCells();
			foreach (Vector2Int noneCell in noneCells)
				map.TrySetFree(noneCell);
		}
	}
}