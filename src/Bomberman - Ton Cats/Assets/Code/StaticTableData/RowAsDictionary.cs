using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StaticTableData
{
	class RowAsDictionary : IReadOnlyDictionary<string, float>
	{
		readonly SimpleFloatTable _table;
		readonly int _rowIndex;

		public RowAsDictionary(SimpleFloatTable table, int rowIndex)
		{
			_table = table;
			_rowIndex = rowIndex;
		}

		public float this[string key]
		{
			get
			{
				if (_table.TryGetValue(key, _rowIndex, out var result) == false)
				{
					Debug.LogError($"Cannot to get: \"{key}\".");
					return default;
				}
				return result;
			}
		}

		public IEnumerable<string> Keys
		{
			get => _table.GetColumnNames();
		}

		public IEnumerable<float> Values
		{
			get
			{
				var colCount = Count;
				for (int c = 0; c < colCount; c++)
				{
					yield return _table.GetValue(c, _rowIndex);
				}
			}
		}

		public int Count
		{
			get => _table.ColumnCount;
		}

		public bool ContainsKey(string key) =>
			_table.HasColumn(key);

		public bool TryGetValue(string key, out float value) =>
			_table.TryGetValue(key, _rowIndex, out value);

		public IEnumerator<KeyValuePair<string, float>> GetEnumerator()
		{
			foreach (var key in _table.GetColumnNames())
			{
				TryGetValue(key, out var value);
				yield return new(key, value);
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}