using System.Collections.Generic;
using System.Linq;
using Extensions;
using Gameplay.Feature.Bonus.Factory;
using Gameplay.Feature.Bonus.StaticData;
using Gameplay.Map;
using Gameplay.Progress;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.MapGenerator.Services.SubGenerator
{
	public sealed class BonusGenerator
	{
		[Inject] IBonusFactory _bonusFactory;
		[Inject] IProgressService _progressService;
		[Inject] IBonusesForLevel _bonusesForLevel;
		[Inject] IAdditionalBombBonuses _additionalBombBonuses;

		public void Create(IGrid<TileType> tilesGrid, IGrid<MapItem> mapItemsGrid,
			IGrid<string> bonusesGrid)
		{
			if (_bonusesForLevel.TryGetBonuses(_progressService.ReachedLevel,
				    out var bonuses) == false)
			{
				Debug.LogError("Cannot to get bonuses for level.");
				return;
			}

			var availableCells = AvailableCells(tilesGrid);
			foreach (var pair in bonuses)
				if (!TryCreateBonuses(pair, mapItemsGrid, bonusesGrid, availableCells))
					continue;
		}

		bool TryCreateBonuses(KeyValuePair<string, int> bonusPair,
			IGrid<MapItem> mapItemsGrid, IGrid<string> bonusesGrid,
			List<Vector2Int> availableCells)
		{
			for (int i = 0; i < bonusPair.Value; i++)
			{
				if (!TryCreateBonus(bonusPair.Key, availableCells, mapItemsGrid,
					    bonusesGrid))
					return false;
			}
			return true;
		}

		bool TryCreateBonus(string bonusType, List<Vector2Int> availableCells,
			IGrid<MapItem> mapItemsGrid, IGrid<string> bonusesGrid)
		{
			if (availableCells.Count() == 0)
			{
				Debug.LogError("Cannot to spawn all bonuses. " +
					"Not enough space for bonuses.");
				return false;
			}

			var cell = ExtractRandomCell(availableCells);

			if (mapItemsGrid.TrySet(MapItem.Bonus, cell) == false
			    || bonusesGrid.TrySet(bonusType, cell) == false)
			{
				Debug.LogError("Cannot to set bonus.");
				return false;
			}

			_bonusFactory.CreateBonusEntity(bonusType, cell);
			return true;
		}

		List<Vector2Int> AvailableCells(IGrid<TileType> tilesGrid) =>
			tilesGrid.AllCoordinates(TileType.Destructible).ToList();

		Vector2Int ExtractRandomCell(List<Vector2Int> availableCells)
		{
			var cell = availableCells.GetRandom(out var index);
			availableCells.RemoveAt(index);
			return cell;
		}
	}
}