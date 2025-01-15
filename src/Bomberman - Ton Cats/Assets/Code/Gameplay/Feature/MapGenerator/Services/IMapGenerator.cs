using System.Collections.Generic;
using Gameplay.Map;
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
		void CreateEnemySpawnCells();
		void CreateDestructibleWalls();
		void CreateBonuses();
		void SetNoneAsFree();
		IGrid<string> EnemySpawnGrid { get; }
	}
}