using System;
using Cysharp.Threading.Tasks;
using Feature.MapGenerator.StaticData;
using Gameplay.Map;
using Zenject;

namespace Feature.MapGenerator.Service
{
	public sealed class StandardMapGenerator : IMapGenerator
	{
		[Inject] IMapData _mapData;
		[Inject] ICellCreator _cellCreator;
		[Inject] IMapGenerator _mapGenerator;
		[Inject] AutoResetUniTaskCompletionSource<IMap> _generateMapCompletionSource;

		public async UniTask<IMap> CreateMap(IGenerateMapProgress progress)
		{
			var size = _mapData.MapSize;
			var map = new StandardTileMap(new TileGrid(size.x, size.y));

			CreateWallOutLine(map);
			CreateIndestructibleWalls(map);
			// CreatePlayer.
			// CreateEnemies.
			CreateDestructibleWalls(map);
			// CreatePowerUps
			BindNavMesh();

			progress.Report(1);
			return await _generateMapCompletionSource.Task;
		}

		void CreateWallOutLine(StandardTileMap map)
		{
			throw new NotImplementedException();
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