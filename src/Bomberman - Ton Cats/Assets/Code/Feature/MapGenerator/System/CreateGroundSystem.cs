using GameTileMap;
using Leopotam.EcsLite;
using LevelData;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateGroundSystem : IEcsRunSystem
	{
		[Inject] IGameTileMap _tileMap;
		[Inject] ILevelData _levelData;

		public void Run(IEcsSystems systems)
		{
			var map = _levelData.Map;
			foreach (var coordinate in map.AllCoordinates())
				_tileMap.SetGroundTile(coordinate);
		}
	}
}