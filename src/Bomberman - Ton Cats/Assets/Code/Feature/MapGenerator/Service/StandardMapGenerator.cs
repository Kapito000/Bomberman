using System;
using Cysharp.Threading.Tasks;
using Feature.MapGenerator.Service.IndestructibleWallsGenerator;
using Feature.MapGenerator.Service.OutLineWallGenerator;
using Feature.MapGenerator.Service.PlayerSpawnGenerator;
using Feature.MapGenerator.StaticData;
using Gameplay.Map;
using UnityEngine;
using Zenject;

namespace Feature.MapGenerator.Service
{
	public sealed class StandardMapGenerator : IMapGenerator
	{
		[Inject] IMapData _mapData;
		
		IHeroSpawnGenerator _heroSpawnGenerator;
		IOutLineWallGenerator _outLineWallGenerator;
		IIndestructibleTilesGenerator _indestructibleTilesGenerator;

		AutoResetUniTaskCompletionSource<IMap> _generateMapCompletionSource;
		
		float _generateProgress;
		IGenerateMapProgress _progressReporter;
		
		const float c_generateProgressStep = .1f;

		public StandardMapGenerator()
		{
			_generateMapCompletionSource = AutoResetUniTaskCompletionSource<IMap>
				.Create();
			_outLineWallGenerator = new StandardOutLineWallGenerator();
			_indestructibleTilesGenerator = new StandardIndestructibleTilesGenerator();
			_heroSpawnGenerator = new StandardHeroSpawnGenerator();
		}

		public async UniTask<IMap> CreateMapAsync(IGenerateMapProgress progressReporter)
		{
			_generateProgress = 0;
			_progressReporter = progressReporter;
			
			var size = _mapData.MapSize + new Vector2Int(2, 2);
			var map = new StandardTileMap(new TileGrid(size.x, size.y));
			MakeProgressStep();

			CreateWallOutLine(map);
			MakeProgressStep();
			CreateIndestructibleWalls(map);
			MakeProgressStep();
			CreatePlayerSpawnArea(map);
			MakeProgressStep();
			// CreateEnemies.
			// CreateDestructibleWalls(map);
			// CreatePowerUps
			// BindNavMesh();

			progressReporter.Report(1);
			_generateMapCompletionSource.TrySetResult(map);
			return await _generateMapCompletionSource.Task;
		}

		void CreateWallOutLine(IMap map) =>
			_outLineWallGenerator.Create(map);

		void CreateIndestructibleWalls(IMap map) =>
			_indestructibleTilesGenerator.Create(map);

		void CreatePlayerSpawnArea(IMap map)
		{
			_heroSpawnGenerator.CreateSpawnArea(map);
		}

		void CreateDestructibleWalls(StandardTileMap map)
		{
			throw new NotImplementedException();
		}

		void BindNavMesh()
		{
			throw new NotImplementedException();
		}

		void MakeProgressStep()
		{
			_generateProgress += c_generateProgressStep;
			_progressReporter.Report(_generateProgress);
		}
	}
}