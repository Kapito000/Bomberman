using System.Linq;
using Gameplay.Feature.FinishLevel.Factory;
using Gameplay.Feature.Map.MapController;
using Gameplay.Map;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.FinishLevel.System
{
	public sealed class CreateFinishLevelDoorSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _spawnRequest;
		[Inject] IMapController _mapController;
		[Inject] IFinishLevelFactory _finishLevelFactory;
		
		public void Run(IEcsSystems systems)
		{
			var freeCells = _mapController.AllCoordinates(CellType.Free).ToArray();
			if (freeCells.Length == 0)
			{
				Debug.LogWarning("Cannot to spawn the finish level door.");
				return;
			}

			var pos = SpawnPosition(freeCells);
			_finishLevelFactory.CreateFinishLevelDoor(pos);
		}

		Vector2 SpawnPosition(Vector2Int[] freeCells)
		{
			var index = Random.Range(0, freeCells.Length);
			var cellPos = freeCells[index];
			var pos = _mapController.GetCellCenterWorld(cellPos);
			return pos;
		}
	}
}