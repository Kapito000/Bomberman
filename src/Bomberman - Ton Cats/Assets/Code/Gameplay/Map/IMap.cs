using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Map
{
	public interface IMap
	{
		Vector2Int Size { get; }
		Vector2Int HeroSpawnPoint { get; }
		bool Has(Vector2Int pos);
		bool Has(CellType type, Vector2Int pos);
		bool TrySetCell(CellType type, Vector2Int pos);
		bool TrySetHeroSpawnPoint(Vector2Int pos);
		CellType GetCellType(Vector2Int pos);
		IEnumerable<Vector2Int> AllCoordinates();
		IEnumerable<Vector2Int> AllCoordinates(CellType type);
	}
}