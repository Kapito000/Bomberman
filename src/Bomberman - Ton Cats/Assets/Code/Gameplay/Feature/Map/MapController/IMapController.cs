using System.Collections.Generic;
using Gameplay.Map;
using UnityEngine;

namespace Gameplay.Feature.Map.MapController
{
	public interface IMapController
	{
		Vector2Int HeroSpawnPoint { get; }
		bool IsFree(Vector2Int pos);
		void SetMap(IMap map);
		Vector2Int WorldToCell(Vector2 pos);
		Vector2 GetCellCenterWorld(Vector2Int cellPos);
		CellType GetCellType(Vector2Int pos);
		IEnumerable<Vector2Int> AllCoordinates();
		void DestroyTile(Vector2Int cellPos);
		void TrySetCell(CellType type, Vector2Int pos);
	}
}