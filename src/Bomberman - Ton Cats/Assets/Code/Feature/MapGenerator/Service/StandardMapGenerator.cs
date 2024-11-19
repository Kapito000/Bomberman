using System;
using Cysharp.Threading.Tasks;
using Feature.MapGenerator.StaticData;
using Gameplay.Map;
using UnityEngine;
using Zenject;

namespace Feature.MapGenerator.Service
{
	public sealed class StandardMapGenerator : IMapGenerator
	{
		[Inject] IMapData _mapData;

		AutoResetUniTaskCompletionSource<IMap> _generateMapCompletionSource;

		public StandardMapGenerator()
		{
			_generateMapCompletionSource = AutoResetUniTaskCompletionSource<IMap>
				.Create();
		}

		public async UniTask<IMap> CreateMapAsync(IGenerateMapProgress progress)
		{
			var size = _mapData.MapSize + new Vector2Int(2, 2);
			var map = new StandardTileMap(new TileGrid(size.x, size.y));

			CreateWallOutLine(map);
			// CreateIndestructibleWalls(map);
			// CreatePlayer.
			// CreateEnemies.
			// CreateDestructibleWalls(map);
			// CreatePowerUps
			// BindNavMesh();

			progress.Report(1);
			_generateMapCompletionSource.TrySetResult(map);
			return await _generateMapCompletionSource.Task;
		}

		void CreateWallOutLine(StandardTileMap map)
		{
			var upperWall = map.Size.y - 1;
			var lowerWall = 0;
			for (var x = 0; x < map.Size.x; x++)
			{
				map.TrySetIndestructible(x, upperWall);
				map.TrySetIndestructible(x, lowerWall);
			}

			var leftWall = 0;
			var rightWall = map.Size.x - 1;
			for (int y = 1; y < map.Size.y - 1; y++)
			{
				map.TrySetIndestructible(leftWall, y);
				map.TrySetIndestructible(rightWall, y);
			}
		}

		void CreateIndestructibleWalls(StandardTileMap map)
		{
			throw new NotImplementedException();
		}

		void CreateDestructibleWalls(StandardTileMap map)
		{
			throw new NotImplementedException();
		}

		void BindNavMesh()
		{
			throw new NotImplementedException();
		}
	}
}