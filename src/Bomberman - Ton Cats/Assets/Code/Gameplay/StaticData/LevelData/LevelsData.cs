using System.Collections.Generic;
using Common.Dictionary;
using Static_table_data;
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

		IReadOnlyDictionary<string, float>[] _bonuses;
		IReadOnlyDictionary<string, float>[] _enemiesAtDoor;
		IReadOnlyDictionary<string, float>[] _enemiesAtStart;

		public IReadOnlyDictionary<string, float>[] Bonuses => _bonuses;
		public IReadOnlyDictionary<string, float>[] EnemiesAtDoor => _enemiesAtDoor;
		public IReadOnlyDictionary<string, float>[] EnemiesAtStart => _enemiesAtStart;

		public void Init()
		{
			ParseData();
		}

		void ParseData()
		{
			ParseTable(_bonusesTable, ref _bonuses);
			ParseTable(_enemiesAtDoorTable, ref _enemiesAtDoor);
			ParseTable(_enemiesAtStartTable, ref _enemiesAtStart);
		}

		void ParseTable(TextAsset textAsset,
			ref IReadOnlyDictionary<string, float>[] levels)
		{
			var floatTable = FloatTable(textAsset);
			CacheRow(floatTable, ref levels);
		}

		void CacheRow(SimpleFloatTable floatTable,
			ref IReadOnlyDictionary<string, float>[] levels)
		{
			levels = new IReadOnlyDictionary<string, float>[floatTable.RowCount];
			for (int rowIndex = 0; rowIndex < levels.Length; rowIndex++)
				floatTable.TryGetRowDictionary(rowIndex, out levels[rowIndex]);
		}

		SimpleFloatTable FloatTable(TextAsset textAsset) =>
			TableFactory.ParseXSV(textAsset.text,
				SimpleFloatTable.SeparatorType.Tab);
	}
}