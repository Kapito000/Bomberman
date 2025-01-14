using Infrastructure.Installer;
using StaticTableData;
using UnityEngine;
using Zenject;
using MappedSpan =
	System.Collections.Generic.IReadOnlyDictionary<string, float>;

namespace Gameplay.Feature.Bomb.StaticData
{
	public sealed class BombDataService : IBombDataService
	{
		[Inject(Id = TsvData.BombData)] TextAsset _tsv;

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