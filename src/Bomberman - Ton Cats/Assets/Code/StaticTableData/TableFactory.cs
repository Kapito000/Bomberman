using System;
using System.Collections.Generic;
using UnityEngine;

namespace StaticTableData {
    public static class TableFactory {
        static List<string> _rowNamesCache = new(16);
        static List<string[]> _dataCache = new(16);

        public static SimpleFloatTable ParseXSV(string fileText, SimpleFloatTable.SeparatorType sepType) {
            using var reader = new System.IO.StringReader(fileText);
            var lines = ReadLines(reader);
            return ParseXSVNumbers(lines, sepType);
        }

        public static SimpleFloatTable ParseXSVNumbers(
            IEnumerable<string> lines,
            SimpleFloatTable.SeparatorType sepType,
            IFloatTable.NavigationType navType = IFloatTable.NavigationType.NamedColumns,
            SimpleFloatTable.OutOfRangePolicy oorPolicy = SimpleFloatTable.OutOfRangePolicy.ClampIndex) {

            var (valueRows, columnNames, rowNames) = ExtractTableData(lines, sepType, navType);

            if (valueRows == null ) {
                return new(0, 0, null, oorPolicy);
            }

            // debug purposes
            int cachedCol = -1, cachedRow = -1;

            try {
                float valueGetter(int colIdx, int rowIdx) {
                    cachedCol = colIdx;
                    cachedRow = rowIdx;
                    var rowValues = valueRows[rowIdx];
                    var rawValue = rowValues[colIdx];
                    var parsedValue = float.Parse(rawValue,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture);
                    return parsedValue;
                }

                const IFloatTable.NavigationType
                    namedColumns = IFloatTable.NavigationType.NamedColumns,
                    namedRows = IFloatTable.NavigationType.NamedRows,
                    named2D = IFloatTable.NavigationType.NamedColumns | IFloatTable.NavigationType.NamedRows;

                var columnCount = 0;
                if (columnNames != null) {
                    columnCount = columnNames.Length;
                } else if (valueRows.Count > 0) {
                    // if we have data without column names, first line is always representative
                    columnCount = valueRows[0].Length;
                }

                return navType switch {
                    namedColumns => new(columnNames, valueRows.Count, valueGetter, oorPolicy),
                    namedRows => new(columnCount, rowNames, valueGetter, oorPolicy),
                    named2D => new(columnNames, rowNames, valueGetter, oorPolicy),
                    _ => new(columnCount, valueRows.Count, valueGetter, oorPolicy),
                };
            } catch (FormatException fe) {
                throw new InvalidOperationException($"Can't parse XSV data: {fe.Message}", fe);
            } catch (Exception ex) {
                //
                throw new Exception($"unexpected {ex.GetType()} at {cachedCol},{cachedRow}", ex);
            }
        }

        static (List<string[]> valueRows, string[] headers, string[] rowNames) ExtractTableData(
            IEnumerable<string> lines,
            SimpleFloatTable.SeparatorType sepType,
            IFloatTable.NavigationType navType = IFloatTable.NavigationType.NamedColumns) {

            // data validation 1: has rows
            using var e = lines.GetEnumerator();
            if (false == e.MoveNext()) {
                return default;
            }

            // data validation 2: has values in row
            var headLine = e.Current;
            var headers = headLine.Split((char)sepType);
            var colCount = headers.Length;
            if (colCount < 1) {
                return default;
            }

            // temporary storage for splitted cell values
            var valueRows = _dataCache;
            valueRows.Clear();

            // container for row names
            var rowNamesContainer = _rowNamesCache;
            // if we assume named rows, we not count 0th value in line as value
            var lineStartIdx = 0;
            if (navType.HasFlag(IFloatTable.NavigationType.NamedRows)) {
                lineStartIdx = 1;
                // clear is only needed on this case; otherwise - don't care
                rowNamesContainer.Clear();
            }

            if (navType.HasFlag(IFloatTable.NavigationType.NamedColumns)) {
                // not count A1 as column name if we use row names
                if (lineStartIdx != 0) headers = headers[lineStartIdx..];
            } else {
                // NOTE: headers is values, if we not use them for navigation
                valueRows.Add(headers[lineStartIdx..]);
                headers = null;
            }

            while (e.MoveNext()) {
                var line = e.Current;

                if (string.IsNullOrWhiteSpace(line)) {
                    // empty lines allowed
                    continue;
                }

                var splittedLine = line.Split((char)sepType);
                if (splittedLine.Length < colCount) {
#if DEBUG
                    Debug.LogError("invalid line!");
#endif
                    break;
                }

                // same as navType.HasFlag(NavigationType.NamedRows) but shorter and maybe faster
                if (lineStartIdx > 0) {
                    // we can count name position always as zero but maybe we should use that smart:
                    rowNamesContainer.Add(splittedLine[lineStartIdx - 1]);
                }
                valueRows.Add(splittedLine[lineStartIdx..]);
            }

            string[] rowNames = null;
            if (lineStartIdx > 0)
                rowNames = rowNamesContainer.ToArray();

            return (valueRows, headers, rowNames);
        }

        static IEnumerable<string> ReadLines(System.IO.StringReader reader) {
            string line;
            do {
                line = reader.ReadLine();
                yield return line;
            } while (line != null);
        }
    }
}
