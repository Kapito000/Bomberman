using Sirenix.OdinInspector;
using UnityEngine;

namespace Static_table_data
{
	public class TableTest : MonoBehaviour
	{
		public TextAsset TableText;

		[Button]
		public void Test()
		{
			var table = TableFactory.ParseXSV(TableText.text,
				SimpleFloatTable.SeparatorType.Tab);
			for (int c = 0; c < table.ColumnCount; c++)
			for (int r = 0; r < table.RowCount; r++)
			{
				Debug.Log($"c: {c}; r: {r}; value: {table.GetValue(c, r)}");
			}
		}
	}
}