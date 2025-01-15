using Extensions;
using Gameplay.Feature.FinishLevel.Component;
using Gameplay.Feature.FinishLevel.Factory;
using Gameplay.Feature.Map.Component;
using Gameplay.Feature.Map.MapController;
using Gameplay.Map;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Map.System
{
	public sealed class SpawnFinishLevelDoorSystem : IEcsRunSystem
	{
		[Inject] EcsWorld _world;
		[Inject] EntityWrapper _destroyedTile;
		[Inject] IMapController _mapController;
		[Inject] IFinishLevelFactory _finishLevelFactory;

		readonly EcsFilterInject<Inc<FinishLevelDoor>> _finishLevelDoorFilter;
		readonly EcsFilterInject<Inc<DestroyedTile, CellPos>> _destroyedTileFilter;

		readonly EcsFilterInject<Inc<FinishLevelDoor, OpenEvent>>
			_openDoorEventFilter;

		public void Run(IEcsSystems systems)
		{
			Cleanup();

			foreach (var tileEntity in _destroyedTileFilter.Value)
			foreach (var finishLevelDoorEntity in _finishLevelDoorFilter.Value)
			{
				_destroyedTile.SetEntity(tileEntity);
				var cell = _destroyedTile.CellPos();
				if (_mapController.TryGet(cell, out MapItem type) &&
				    type == MapItem.FinishLevelDoor)
				{
					var pos = _mapController.GetCellCenterWorld(cell);
					_finishLevelFactory.CreateFinishLevelDoor(finishLevelDoorEntity, pos);
				}
			}
		}

		void Cleanup()
		{
			foreach (var e in _openDoorEventFilter.Value)
				_world.RemoveComponent<OpenEvent>(e);
		}
	}
}