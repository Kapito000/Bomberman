using Common.Dictionary;
using Sirenix.OdinInspector;
using Static_table_data;
using UnityEngine;
using UnityEngine.Serialization;
using Menu = Constant.CreateAssetMenu.Path;

namespace Gameplay.StaticData.LevelData
{
	[CreateAssetMenu(menuName = Menu.c_StaticData + nameof(LevelsData))]
	public sealed class LevelsData : ScriptableObject, ILevelsData
	{
		[SerializeField] TextAsset _bonusesTable;
		[SerializeField] TextAsset _enemiesAtDoorTable;
		[SerializeField] TextAsset _enemiesAtStartTable;
		[Space]
		[SerializeField] StringIntegerDictionary[] _bonuses;
		[SerializeField] StringIntegerDictionary[] _enemiesAtDoor;
		[SerializeField] StringIntegerDictionary[] _enemiesAtStart;
		
		public StringIntegerDictionary[] Bonuses => _bonuses;
		public StringIntegerDictionary[] EnemiesAtDoor => _enemiesAtDoor;
		public StringIntegerDictionary[] EnemiesAtStart => _enemiesAtStart;

		[Button]
		public void ParseData()
		{
			ParseTable(_bonusesTable, ref _bonuses, TableColumnName.c_PowerfulBomb);
			var enemyNames = new[]
			{
				TableColumnName.Enemy.c_Assassin,
				TableColumnName.Enemy.c_Cyber,
				TableColumnName.Enemy.c_Flash,
				TableColumnName.Enemy.c_Hologram,
				TableColumnName.Enemy.c_Scammer,
			};
			ParseTable(_enemiesAtDoorTable, ref _enemiesAtDoor, enemyNames);
			ParseTable(_enemiesAtStartTable, ref _enemiesAtStart, enemyNames);
		}

		void ParseTable(TextAsset textAsset, ref StringIntegerDictionary[] levels,
			params string[] columnNames)
		{
			var floatTable = FloatTable(textAsset);
			CreateArray(out levels, floatTable);
			foreach (var columnName in columnNames)
				ParseColumn(columnName, floatTable, levels);
		}

		void CreateArray(out StringIntegerDictionary[] levels,
			SimpleFloatTable floatTable)
		{
			levels = new StringIntegerDictionary[floatTable.RowCount];
			for (int i = 0; i < levels.Length; i++)
				levels[i] = new StringIntegerDictionary();
		}

		void ParseColumn(string columnName, SimpleFloatTable floatTable,
			StringIntegerDictionary[] levels)
		{
			if (floatTable.HasColumn(columnName) == false)
			{
				Debug.LogError($"The table \"{_bonusesTable.name}\" not contains " +
					$"the column \"{columnName}\".");
				return;
			}

			for (int r = 0; r < floatTable.RowCount; r++)
				if (floatTable.TryGetValue(columnName, r, out float value))
				{
					if (value == 0) continue;
					levels[r].Add(columnName, (int)value);
				}
		}

		SimpleFloatTable FloatTable(TextAsset textAsset) =>
			TableFactory.ParseXSV(textAsset.text,
				SimpleFloatTable.SeparatorType.Tab);
	}
}