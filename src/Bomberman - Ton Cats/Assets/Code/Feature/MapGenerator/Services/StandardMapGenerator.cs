using System;
using Feature.MapGenerator.Services.DestructibleTilesGenerator;
using Feature.MapGenerator.Services.EnemySpawnGenerator;
using Feature.MapGenerator.Services.HeroSpawnGenerator;
using Feature.MapGenerator.Services.IndestructibleWallsGenerator;
using Feature.MapGenerator.Services.OutLineWallGenerator;
using Feature.MapGenerator.StaticData;
using Gameplay.Map;
using UnityEngine;

namespace Feature.MapGenerator.Services
{
	public sealed class StandardMapGenerator : IMapGenerator
	{
		IMapData _mapData;

		IHeroSpawnGenerator _heroSpawnGenerator;
		IEnemySpawnGenerator _enemySpawnGenerator;
		IOutLineWallGenerator _outLineWallGenerator;
		IDestructibleTilesGenerator _destructibleTilesGenerator;
		IIndestructibleTilesGenerator _indestructibleTilesGenerator;

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
			var map = new StandardTileMap(new TileGrid(size.x, size.y));
			CreateWallOutLine(map);
			CreateIndestructibleWalls(map);
			CreatePlayerSpawnArea(map);
			CreateEnemies(map);
			CreateDestructibleWalls(map);
			// CreatePowerUps
			// BindNavMesh();
			return map;
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

		void BindNavMesh()
		{
			throw new NotImplementedException();
		}
	}
}