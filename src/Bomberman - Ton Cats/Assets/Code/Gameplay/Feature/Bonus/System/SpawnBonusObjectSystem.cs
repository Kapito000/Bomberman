using Gameplay.Feature.Bonus.Component;
using Gameplay.Feature.Bonus.Factory;
using Gameplay.Feature.Map.Component;
using Gameplay.Feature.Map.MapController;
using Gameplay.Map;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Bonus.System
{
	public sealed class SpawnBonusObjectSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _tile;
		
		[Inject] IBonusFactory _bonusFactory;
		[Inject] IMapController _mapController;

		readonly EcsFilterInject<Inc<BonusComponent, BonusType>> _bonusesFilter;
		readonly EcsFilterInject<Inc<DestroyedTile, CellPos>> _destroyedTileFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var tileEntity in _destroyedTileFilter.Value)
			foreach (var bonusEntity in _bonusesFilter.Value)
			{
				_tile.SetEntity(tileEntity);

				var tileCell = _tile.CellPos();
				if (_mapController.TryGet(tileCell, out MapItem item)
				    && item == MapItem.Bonus)
				{
					var pos = _mapController.GetCellCenterWorld(tileCell);
					_bonusFactory.CreateBonusObject(pos, bonusEntity);
					_mapController.RemoveItem(tileCell);
				}
			}
		}
	}
}