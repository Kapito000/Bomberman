using System.Collections.Generic;
using Infrastructure;
using UnityEngine;

namespace Gameplay.Feature.MapGenerator.Services
{
	public interface IMapGenerator : IService
	{
		void CreateMap();
		void CreateGroundTiles();
		void CreateIndestructibleTiles();
		Vector2Int CreateHeroSpawnCell();
		void CreateHeroSafeArea();
		IEnumerable<Vector2Int> EnemySpawnCells();
		void CreateEnemySafeArea();
		void CreateDestructibleWalls();
		void SetNoneAsFree();
	}
}