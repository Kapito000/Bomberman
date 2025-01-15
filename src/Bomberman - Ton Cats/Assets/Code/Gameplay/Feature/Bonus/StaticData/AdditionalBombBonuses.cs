using System;
using System.Collections.Generic;
using Gameplay.Feature.Bomb;
using StaticTableData;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Bonus.StaticData
{
	public sealed class AdditionalBombBonuses : IAdditionalBombBonuses
	{
		[Inject(Id = Constant.TsvDataId.c_AdditionalBombBonuses)]
		TextAsset _tsvTable;

		IFloatTable _table;

		public void Init()
		{
			var navType = IFloatTable.NavigationType.NamedColumns;
			_table = TableFactory.ParseXSV(_tsvTable.text,
				SimpleFloatTable.SeparatorType.Tab, navType);
		}

		public bool TryGetBombs(int level,
			out IReadOnlyDictionary<BombType, int> bombs)
		{
			var result = new Dictionary<BombType, int>();
			bombs = result;

			if (level >= _table.ColumnCount)
				level = _table.ColumnCount - 1;

			if (_table.TryGetMappedRow(level, out var dictionary) == false)
				return false;

			foreach (var pair in dictionary)
			{
				if (pair.Value == 0)
					continue;

				if (Enum.TryParse(pair.Key, out BombType type) == false)
				{
					Debug.LogError(
						$"Cannot convert the bomb id\"{pair.Key}\" " +
						$"to \"{nameof(BombType)}\".");
					continue;
				}

				result.Add(type, (int)pair.Value);
			}

			return true;
		}
	}
}