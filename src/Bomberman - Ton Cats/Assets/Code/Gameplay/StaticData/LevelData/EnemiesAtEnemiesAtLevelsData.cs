using System.Collections.Generic;
using StaticTableData;
using UnityEngine;
using Zenject;

namespace Gameplay.StaticData.LevelData
{
	public sealed class EnemiesAtEnemiesAtLevelsData : IEnemiesAtLevelsData
	{
		[Inject(Id = Constant.TsvDataId.c_EnemiesAtDoor)]
		TextAsset _enemiesAtDoorTable;
		[Inject(Id = Constant.TsvDataId.c_EnemiesAtStart)]
		TextAsset _enemiesAtStartTable;

		Dictionary<Table, SimpleFloatTable> _tables = new();

		public void Init()
		{
			ParseData();
		}

		public bool TryGetRow(Table tableKey, int rowIndex,
			out IReadOnlyDictionary<string, float> row)
		{
			if (_tables.TryGetValue(tableKey, out var table) == false)
			{
				CastCannotToGetDataMessage();
				row = default;
				return false;
			}

			if (table.TryGetMappedRow(rowIndex, out row) == false)
			{
				CastCannotToGetDataMessage();
				return false;
			}

			return true;
		}

		public bool TryGetLastLevelFor(Table tableKey, out int lastLevel)
		{
			if (_tables.TryGetValue(tableKey, out var table) == false)
			{
				lastLevel = default;
				return false;
			}

			lastLevel = table.RowCount;
			return true;
		}

		void ParseData()
		{
			ParseTable(Table.EnemiesAtDoor, _enemiesAtDoorTable);
			ParseTable(Table.EnemiesAtStart, _enemiesAtStartTable);
		}

		void ParseTable(Table tableKey, TextAsset textAsset)
		{
			SimpleFloatTable table = FloatTable(textAsset);
			_tables.Add(tableKey, table);
		}

		SimpleFloatTable FloatTable(TextAsset textAsset)
		{
			var navType = IFloatTable.NavigationType.NamedColumns;
			return TableFactory.ParseXSV(textAsset.text,
				SimpleFloatTable.SeparatorType.Tab, navType);
		}

		void CastCannotToGetDataMessage() =>
			Debug.LogError("Cannot to get the table data.");
	}
}