using System.Linq;
using Gameplay.Feature.FinishLevel.Factory;
using Gameplay.Feature.Map.MapController;
using Gameplay.Map;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.MapGenerator.System
{
	public sealed class CreateFinishLevelDoorSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _spawnRequest;
		[Inject] IMapController _mapController;
		[Inject] IFinishLevelFactory _finishLevelFactory;

		public void Run(IEcsSystems systems)
		{
			var destructibles = _mapController
				.AllCoordinates(TileType.Destructible)
				.ToArray();
			if (destructibles.Length == 0)
			{
				Debug.LogWarning("Cannot to spawn the finish level door. " +
					"Has no any destructible cell.");
				return;
			}

			var cell = SpawnCellPos(destructibles);
			_mapController.TrySet(MapItem.FinishLevelDoor, cell);
			_finishLevelFactory.CreateFinishLevelDoorEntity(cell);
		}

		Vector2Int SpawnCellPos(Vector2Int[] cells)
		{
			var index = Random.Range(0, cells.Length);
			var cell = cells[index];
			return cell;
		}
	}
}