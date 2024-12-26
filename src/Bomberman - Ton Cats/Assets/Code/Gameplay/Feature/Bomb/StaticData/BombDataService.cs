using StaticTableData;
using UnityEngine;
using Menu = Constant.CreateAssetMenu.Path;
using MappedSpan =
	System.Collections.Generic.IReadOnlyDictionary<string, float>;

namespace Gameplay.Feature.Bomb.StaticData
{
	[CreateAssetMenu(menuName = Menu.c_StaticData + nameof(BombDataService))]
	public sealed class BombDataService : ScriptableObject, IBombDataService
	{
		[SerializeField] TextAsset _tsv;

		SimpleFloatTable _table;

		public void Init()
		{
			var navigationType = IFloatTable.NavigationType.NamedColumns |
				IFloatTable.NavigationType.NamedRows;
			_table = TableFactory.ParseXSV(_tsv.text,
				SimpleFloatTable.SeparatorType.Tab, navigationType);
		}

		public bool TryGet(BombType bombType, BombCharacteristic characteristic,
			out float value)
		{
			var successful = _table
				.TryGetValue(characteristic.ToString(), bombType.ToString(), out value);
			if (successful == false)
			{
				Debug.LogError($"Cannot ot get the \"{characteristic.ToString()}\" " +
					$"for the \"{bombType.ToString()}\".");
				return false;
			}

			return true;
		}

		public bool TryGetCharacteristic(BombType bombType,
			out MappedSpan characteristics)
		{
			var bombName = bombType.ToString();
			var successful = _table
				.TryGetMappedRow(bombName, out characteristics);
			if (successful == false)
			{
				Debug.LogError($"Cannot ot get the characteristics for the " +
					$"\"{bombName}\".");
				return false;
			}

			return true;
		}
	}
}