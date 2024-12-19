using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.Scripting;

namespace Static_table_data
{
	[Preserve]
	public class SimpleFloatTable : IFloatTable
	{
		const int ROW_DIM = 0, COL_DIM = 1;

		readonly Dictionary<string, int> _cachedColumnIds;
		readonly float[,] _values;

		public OutOfRangePolicy OORPolicy = OutOfRangePolicy.DefaultValue;

		public SimpleFloatTable(string[] columns, int rowCount,
			Func<int, int, float> valueGetter,
			OutOfRangePolicy oorPolicy = OutOfRangePolicy.DefaultValue)
		{
			_cachedColumnIds = columns.ToIndexDictionary();
			var colCount = columns.Length;
			OORPolicy = oorPolicy;

			if (colCount * rowCount == 0 || valueGetter == null)
				return;

			_values = ROW_DIM == 0
				? new float[rowCount, colCount]
				: new float[colCount, rowCount];

			for (var col = 0; col < colCount; col++)
			{
				for (var row = 0; row < rowCount; row++)
				{
					SetValueInternal(col, row, valueGetter(col, row));
				}
			}
		}

		public int ColumnCount
		{
			get => GetSize(COL_DIM);
		}
		public int RowCount
		{
			get => GetSize(ROW_DIM);
		}

		public bool IsValid()
		{
			return _values != null
				&& _cachedColumnIds?.Count > 0
				&& _values.GetLength(ROW_DIM) > 0;
		}

		public bool HasColumn(string columnName)
		{
			return _cachedColumnIds.ContainsKey(columnName);
		}

		public IReadOnlyCollection<string> GetColumnNames()
		{
			return _cachedColumnIds.Keys;
		}

		public float GetValue(int columnIdx, int rowIdx)
		{
			return GetValueInternal(columnIdx, rowIdx);
		}

		public bool TryGetValue(string columnName, int row, out float value)
		{
			var success = false;
			value = default;

			if (_values == null || _values.GetLength(ROW_DIM) == 0)
			{
				Debug.LogError("attemp to read data from invalid table");
				return false;
			}

			if (row < 0)
			{
				Debug.LogError($"row index must be >= 0, actually {row}");
				return false;
			}

			var nameExists = _cachedColumnIds.TryGetValue(columnName, out var col);

			if (nameExists)
			{
				var rowsCount = _values.GetLength(ROW_DIM);
				if (rowsCount <= row)
					switch (OORPolicy)
					{
						case OutOfRangePolicy.ClampIndex:
							row = rowsCount - 1;
							break;
						case OutOfRangePolicy.LoopIndex:
							do
							{
								row -= rowsCount;
							} while (row >= rowsCount);
							break;
						case OutOfRangePolicy.DefaultValue:
							return true;
						case OutOfRangePolicy.NotFound:
							return false;
					}

				value = GetValueInternal(col, row);
				success = true;
			}
			return success;
		}

		void SetValueInternal(int col, int row, float value)
		{
			if (ROW_DIM == 0)
			{
				_values[row, col] = value;
			}
			else
			{
#pragma warning disable CS0162
				_values[row, col] = value;
#pragma warning restore CS0162
			}
		}

		float GetValueInternal(int col, int row)
		{
			return ROW_DIM == 0 ? _values[row, col] : _values[col, row];
		}

		int GetSize(int dimension)
		{
			return _values == null ? 0 : _values.GetLength(dimension);
		}

		public enum OutOfRangePolicy
		{
			DefaultValue,
			ClampIndex,
			LoopIndex,
			NotFound
		}

		public enum SeparatorType
		{
			Tab = '\t',
			Comma = ',',
			Semicolon = ';'
		}
	}
}