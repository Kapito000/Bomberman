using GameTileMap;
using Leopotam.EcsLite;
using LevelData;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateIndestructibleTilesSystem : IEcsRunSystem
	{
		[Inject] IGameMap _gameMap;
		[Inject] ILevelData _levelData;

		public void Run(IEcsSystems systems)
		{
			var map = _levelData.Map;
			foreach (var indestructible in map.Indestuctibles)
				_gameMap.SetIndestructibleTile(indestructible);
		}
	}
}