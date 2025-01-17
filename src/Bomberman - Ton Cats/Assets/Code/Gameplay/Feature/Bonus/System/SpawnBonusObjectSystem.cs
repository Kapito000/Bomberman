using Common.Component;
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
		[Inject] EntityWrapper _bonusesParent;
		[Inject] EntityWrapper _bonus;

		[Inject] IBonusFactory _bonusFactory;
		[Inject] IMapController _mapController;

		readonly EcsFilterInject<
			Inc<BonusComponent, BonusType, CellPos>> _bonusesFilter;
		readonly EcsFilterInject<
			Inc<DestroyedTile, CellPos>> _destroyedTileFilter;
		readonly EcsFilterInject<
			Inc<BonusesParent, TransformComponent>> _bonusesParentFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var tileEntity in _destroyedTileFilter.Value)
			foreach (var bonusesParentEntity in _bonusesParentFilter.Value)
			foreach (var bonusEntity in _bonusesFilter.Value)
			{
				_tile.SetEntity(tileEntity);
				_bonus.SetEntity(bonusEntity);
				_bonusesParent.SetEntity(bonusesParentEntity);


				var tileCell = _tile.CellPos();
				var bonusCell = _bonus.CellPos();

				if (tileCell != bonusCell
				    || false == _mapController.TryGet(tileCell, out MapItem item)
				    || item != MapItem.Bonus)
					continue;

				var pos = _mapController.GetCellCenterWorld(tileCell);
				var parent = _bonusesParent.Transform();
				_bonusFactory.CreateBonusObject(pos, bonusEntity, parent);
				_mapController.RemoveItem(tileCell);
			}
		}
	}
}