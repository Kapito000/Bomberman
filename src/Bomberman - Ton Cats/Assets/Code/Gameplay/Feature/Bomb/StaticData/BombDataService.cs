using StaticTableData;
using UnityEngine;
using Menu = Constant.CreateAssetMenu.Path;

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
			var successfulGet = _table
				.TryGetValue(characteristic.ToString(), bombType.ToString(), out value);
			if (successfulGet == false)
			{
				Debug.LogError($"Cannot ot get the \"{characteristic.ToString()}\" " +
					$"for the \"{bombType.ToString()}\".");
				return false;
			}

			return true;
		}
	}
}