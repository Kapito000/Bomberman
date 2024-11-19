using Feature.MapGenerator.Service;
using Gameplay.Map;
using Leopotam.EcsLite;
using MapTile.TileProvider;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateMapSystem : IEcsRunSystem
	{
		[Inject] Tilemap _tilemap;
		[Inject] IMapGenerator _mapGenerator;
		[Inject] ITileProvider _tileProvider;

		public async void Run(IEcsSystems systems)
		{
			var map = await _mapGenerator.CreateMapAsync(new ProgressDebugger());
			var indestructibles = map.Indestuctibles;
			foreach (var indestructible in indestructibles)
			{
				var tile = _tileProvider[CellType.Indestructible];
				_tilemap.SetTile((Vector3Int)indestructible, tile);
			}
		}
	}

	public sealed class ProgressDebugger : IGenerateMapProgress
	{
		public void Report(float value)
		{
			Debug.Log(value);
		}
	}
}