using System;
using System.Collections.Generic;
using UnityEngine;

namespace Static_table_data
{
	public static class TableFactory
	{
		public static SimpleFloatTable ParseXSV(string fileText,
			SimpleFloatTable.SeparatorType sepType)
		{
			using var reader = new System.IO.StringReader(fileText);
			var lines = ReadLines(reader);
			return ParseXSV(lines, sepType);
		}

		public static SimpleFloatTable ParseXSV(IEnumerable<string> lines,
			SimpleFloatTable.SeparatorType sepType)
		{
			Dictionary<string, int> _cachedColumnIds;
			//
			var e = lines.GetEnumerator();
			if (false == e.MoveNext())
			{
				// empty tables instead values
				_cachedColumnIds = new();
				return new SimpleFloatTable(Array.Empty<string>(), 0, null,
					SimpleFloatTable.OutOfRangePolicy.ClampIndex);
			}

			var headLine = e.Current;
			var headers = headLine.Split((char)sepType);
			_cachedColumnIds = new(headers.Length);
			for (int i = 0; i < headers.Length; i++)
			{
				_cachedColumnIds[headers[i]] = i;
			}

			var valueRows = new List<string[]>(8);
			while (e.MoveNext())
			{
				var line = e.Current;

				if (string.IsNullOrWhiteSpace(line))
				{
					// empty lines allowed
					continue;
				}

				var splittedLine = line.Split((char)sepType);
				if (splittedLine.Length < _cachedColumnIds.Count)
				{
#if DEBUG
					Debug.LogError("invalid line!");
#endif
					break;
				}
				valueRows.Add(splittedLine);
			}

			int col = _cachedColumnIds.Count, row = valueRows.Count;
			int cachedCol = -1, cachedRow = -1;

			try
			{
				return new SimpleFloatTable(headers, valueRows.Count,
					(colIdx, rowIdx) =>
					{
						//
						cachedCol = colIdx;
						cachedRow = rowIdx;
						var rowValues = valueRows[rowIdx];
						var rawValue = rowValues[colIdx];
						var parsedValue = float.Parse(rawValue,
							System.Globalization.NumberStyles.Any,
							System.Globalization.CultureInfo.InvariantCulture);
						return parsedValue;
					}, SimpleFloatTable.OutOfRangePolicy.ClampIndex);
			}
			catch (FormatException fe)
			{
				throw new InvalidOperationException(
					$"Can't parse XSV data: {fe.Message}", fe);
			}
			catch (Exception ex)
			{
				//
				throw new Exception(
					$"unexpected {ex.GetType()} at {cachedCol},{cachedRow}", ex);
			}
		}

		static IEnumerable<string> ReadLines(System.IO.StringReader reader)
		{
			string line;
			do
			{
				line = reader.ReadLine();
				yield return line;
			} while (line != null);
		}
	}
}