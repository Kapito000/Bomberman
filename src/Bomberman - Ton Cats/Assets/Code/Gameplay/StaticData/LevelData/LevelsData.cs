using System.Collections.Generic;
using StaticTableData;
using UnityEngine;
using Menu = Constant.CreateAssetMenu.Path;

namespace Gameplay.StaticData.LevelData
{
	[CreateAssetMenu(menuName = Menu.c_StaticData + nameof(LevelsData))]
	public sealed class LevelsData : ScriptableObject, ILevelsData
	{
		[SerializeField] TextAsset _bonusesTable;
		[SerializeField] TextAsset _enemiesAtDoorTable;
		[SerializeField] TextAsset _enemiesAtStartTable;

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
			
			table.GetRowDictionary(rowIndex, out row);

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
			ParseTable(Table.Bonuses, _bonusesTable);
			ParseTable(Table.EnemiesAtDoor, _enemiesAtDoorTable);
			ParseTable(Table.EnemiesAtStart, _enemiesAtStartTable);
		}

		void ParseTable(Table tableKey, TextAsset textAsset)
		{
			SimpleFloatTable table = FloatTable(textAsset);
			_tables.Add(tableKey, table);
		}

		SimpleFloatTable FloatTable(TextAsset textAsset) =>
			TableFactory.ParseXSV(textAsset.text,
				SimpleFloatTable.SeparatorType.Tab);

		void CastCannotToGetDataMessage() =>
			Debug.LogError("Cannot to get the table data.");
	}
}