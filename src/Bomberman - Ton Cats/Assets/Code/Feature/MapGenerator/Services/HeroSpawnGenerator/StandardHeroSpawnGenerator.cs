using Feature.MapGenerator.Services.SafeArea;
using Gameplay.Map;
using UnityEngine;

namespace Feature.MapGenerator.Services.HeroSpawnGenerator
{
	public sealed class StandardHeroSpawnGenerator : IHeroSpawnGenerator
	{
		readonly ISafeAreaCalculator _safeAreaCalculator;

		public StandardHeroSpawnGenerator()
		{
			_safeAreaCalculator = new CrossSafeAreaCalculator();
		}

		public void CreateSpawnArea(IMap map)
		{
			int x = 1;
			int y = map.Size.y - 2;
			var spawnPoint = new Vector2Int(x, y);
			CreateSafeArea(map, spawnPoint);
			map.TrySetHeroSpawnPoint(spawnPoint);
		}

		void CreateSafeArea(IMap map, Vector2Int spawnPoint)
		{
			var safeCells = _safeAreaCalculator.SafeArea(spawnPoint);
			foreach (var cell in safeCells)
			{
				if (map.IsNone(cell))
					map.TrySetFree(cell);
			}
		}
	}
}