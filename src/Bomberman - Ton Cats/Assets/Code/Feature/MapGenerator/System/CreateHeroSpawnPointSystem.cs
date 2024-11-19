using Feature.Hero.Factory;
using Leopotam.EcsLite;
using LevelData;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Feature.MapGenerator.System
{
	public sealed class CreateHeroSpawnPointSystem : IEcsRunSystem
	{
		[Inject] Tilemap _tilemap;
		[Inject] ILevelData _levelData;
		[Inject] IHeroFactory _heroFactory;

		public void Run(IEcsSystems systems)
		{
			var spawnPoint = _levelData.Map.HeroSpawnPoint;
			var pos = _tilemap.GetCellCenterWorld((Vector3Int)spawnPoint);
			_heroFactory.CreateHeroSpawnPoint(pos);
		}
	}
}