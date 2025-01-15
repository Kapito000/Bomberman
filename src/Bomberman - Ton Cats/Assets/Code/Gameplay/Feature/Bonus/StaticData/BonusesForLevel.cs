using System.Collections.Generic;
using StaticTableData;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Bonus.StaticData
{
	public sealed class BonusesForLevel : IBonusesForLevel
	{
		[Inject(Id = Constant.TsvDataId.c_Bonuses)] TextAsset _tsvTable;

		IFloatTable _table;

		public void Init()
		{
			var navType = IFloatTable.NavigationType.NamedColumns;
			_table = TableFactory.ParseXSV(_tsvTable.text,
				SimpleFloatTable.SeparatorType.Tab, navType);
		}

		public bool TryGetBonuses(int level,
			out IReadOnlyDictionary<string, int> bonuses)
		{
			var result = new Dictionary<string, int>();
			bonuses = result;

			if (level >= _table.RowCount)
				level = _table.RowCount - 1;

			if (_table.TryGetMappedRow(level, out var dictionary) == false)
				return false;

			foreach (var pair in dictionary)
			{
				if (pair.Value == 0)
					continue;

				result.Add(pair.Key, (int)pair.Value);
			}

			return true;
		}
	}
}