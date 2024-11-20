using Gameplay.Map;
using Leopotam.EcsLite;
using LevelData;
using MapTile.TileProvider;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateDestructibleTilesSystem : IEcsRunSystem
	{
		[Inject] Tilemap _tilemap;
		[Inject] ILevelData _levelData;
		[Inject] ITileProvider _tileProvider;
		
		public void Run(IEcsSystems systems)
		{
			var map = _levelData.Map;
			foreach (var indestructible in map.Destuctibles)
			{
				var tile = _tileProvider[CellType.Destructible];
				_tilemap.SetTile((Vector3Int)indestructible, tile);
			}
		}

	}
}