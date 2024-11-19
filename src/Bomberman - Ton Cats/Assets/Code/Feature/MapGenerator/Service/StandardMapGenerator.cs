using System;
using Cysharp.Threading.Tasks;
using Feature.MapGenerator.Service.IndestructibleWallsGenerator;
using Feature.MapGenerator.Service.OutLineWallGenerator;
using Feature.MapGenerator.StaticData;
using Gameplay.Map;
using UnityEngine;
using Zenject;

namespace Feature.MapGenerator.Service
{
	public sealed class StandardMapGenerator : IMapGenerator
	{
		[Inject] IMapData _mapData;

		IOutLineWallGenerator _outLineWallGenerator;
		IIndestructibleTilesGenerator _indestructibleTilesGenerator;

		AutoResetUniTaskCompletionSource<IMap> _generateMapCompletionSource;

		public StandardMapGenerator()
		{
			_generateMapCompletionSource = AutoResetUniTaskCompletionSource<IMap>
				.Create();
			_outLineWallGenerator = new StandardOutLineWallGenerator();
			_indestructibleTilesGenerator = new StandardIndestructibleTilesGenerator();
		}

		public async UniTask<IMap> CreateMapAsync(IGenerateMapProgress progress)
		{
			var size = _mapData.MapSize + new Vector2Int(2, 2);
			var map = new StandardTileMap(new TileGrid(size.x, size.y));

			CreateWallOutLine(map);
			CreateIndestructibleWalls(map);
			// CreatePlayer.
			// CreateEnemies.
			// CreateDestructibleWalls(map);
			// CreatePowerUps
			// BindNavMesh();

			progress.Report(1);
			_generateMapCompletionSource.TrySetResult(map);
			return await _generateMapCompletionSource.Task;
		}

		void CreateWallOutLine(IMap map) =>
			_outLineWallGenerator.Create(map);

		void CreateIndestructibleWalls(IMap map) => 
			_indestructibleTilesGenerator.Create(map);

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