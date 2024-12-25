using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine.Scripting;

namespace StaticTableData {
    [Preserve]
    public class SimpleFloatTable : IFloatTable {
        const int ROW_DIM = 0, COL_DIM = 1;

        readonly Dictionary<string, int> _cachedColumnIds;
        readonly Dictionary<string, int> _cachedRowIds;
        readonly float[,] _values;

        // have no naming navigation by default; changes in specific ctors
        readonly IFloatTable.NavigationType _navigationType = IFloatTable.NavigationType.OnlyIndices;

        public OutOfRangePolicy OORPolicy = OutOfRangePolicy.DefaultValue;

        public IFloatTable.NavigationType NavType => _navigationType;

        public SimpleFloatTable(string[] columns, string[] rows,
            Func<int, int, float> valueGetter,
            OutOfRangePolicy oorPolicy = OutOfRangePolicy.DefaultValue)
            : this(columns.Length, rows.Length, valueGetter, oorPolicy) {

            _cachedColumnIds = columns.ToIndexDictionary();
            _cachedRowIds = rows.ToIndexDictionary();
        }

        public SimpleFloatTable(int colCount, string[] rows,
            Func<int, int, float> valueGetter,
            OutOfRangePolicy oorPolicy = OutOfRangePolicy.DefaultValue)
            : this(colCount, rows.Length, valueGetter, oorPolicy) {

            _cachedRowIds = rows.ToIndexDictionary();
        }

        public SimpleFloatTable(string[] columns, int rowCount,
            Func<int, int, float> valueGetter,
            OutOfRangePolicy oorPolicy = OutOfRangePolicy.DefaultValue)
            : this(columns.Length, rowCount, valueGetter, oorPolicy) {

            _cachedColumnIds = columns.ToIndexDictionary();
        }

        public SimpleFloatTable(int colCount, int rowCount, Func<int, int, float> valueGetter,
            OutOfRangePolicy oorPolicy = OutOfRangePolicy.DefaultValue) {
            OORPolicy = oorPolicy;

            if (colCount * rowCount == 0 || valueGetter == null)
                return;

            _values = ROW_DIM == 0 ? new float[rowCount, colCount] : new float[colCount, rowCount];

            for ( var col = 0; col < colCount; col++ ) {
                for ( var row = 0; row < rowCount; row++ ) {
                    SetValueInternal(col, row, valueGetter(col, row));
                }
            }
        }

        public int ColumnCount { get => GetSize(COL_DIM); }
        public int RowCount { get => GetSize(ROW_DIM); }

        public bool IsValid() {
            return _values != null
                && _cachedColumnIds?.Count > 0
                && _values.GetLength(ROW_DIM) > 0;
        }

        public bool HasColumn(string columnName) {
            return _cachedColumnIds.ContainsKey(columnName);
        }

        public IReadOnlyCollection<string>? GetColumnNames() {
            return _cachedColumnIds?.Keys;
        }

        public IReadOnlyCollection<string>? GetRowNames() {
            return _cachedRowIds?.Keys;
        }

        public bool TryGetColumn(string name, out int columnIdx) {
            columnIdx = -1;
            return _cachedColumnIds != null
                && _cachedColumnIds.TryGetValue(name, out columnIdx);
        }

        public bool TryGetRow(string name, out int rowIdx) {
            rowIdx = -1;
            return _cachedRowIds != null
                && _cachedRowIds.TryGetValue(name, out rowIdx);
        }

        public float GetValue(int columnIdx, int rowIdx) {
            return GetValueInternal(columnIdx, rowIdx);
        }

        public float GetValue(string columnName, int row) {
            var success = TryGetValue(columnName, row, out var value);
            if (false == success) {
                throw new ArgumentOutOfRangeException(nameof(columnName), $"no data for column {columnName}!");
            }
            return value;
        }

        public bool GetRowDictionary(int rowIndex, out IReadOnlyDictionary<string, float> dict) {
            if (NavType.HasFlag(IFloatTable.NavigationType.NamedColumns)) {
                dict = null;
                return false;
            }

            const int dimension = COL_DIM; // for row dictionary we iterate over column values inside single row
            return TryGetDimMap(dimension, rowIndex, out dict);
        }

        public bool GetColumnDictionary(int columnIndex, out IReadOnlyDictionary<string, float> dict) {
            if (NavType.HasFlag(IFloatTable.NavigationType.NamedRows)) {
                dict = null;
                return false;
            }

            const int dimension = ROW_DIM; // for column dictionary we iterate over row values inside single column
            return TryGetDimMap(dimension, columnIndex, out dict);
        }

        bool TryGetDimMap(int dimension, int lineIndex, out IReadOnlyDictionary<string, float> dict) {
            if (lineIndex < 0) throw new ArgumentOutOfRangeException( nameof(lineIndex));
            dict = null;
            if (lineIndex < GetSize(dimension)) {
                dict = new D1NavDictionary(this, dimension, lineIndex);
            }
            return dict != null;
        }

        public bool TryGetValue(string columnName, int row, out float value) {
            var success = false;
            value = default;

            if (_values == null || _values.GetLength(ROW_DIM) == 0) {
#if DEBUG
                throw new InvalidOperationException("attemp to read data from invalid table");
#else
                return false;
#endif
            }

            if (row < 0) {
                throw new ArgumentOutOfRangeException($"row index must be >= 0, actually {row}");
            }

            var nameExists = _cachedColumnIds.TryGetValue(columnName, out var col);

            if (nameExists) {
                var rowsCount = _values.GetLength(ROW_DIM);
                if (rowsCount <= row)
                    switch (OORPolicy) {
                        case OutOfRangePolicy.ClampIndex:
                            row = rowsCount - 1;
                            break;
                        case OutOfRangePolicy.LoopIndex:
                            do {
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

        void SetValueInternal(int col, int row, float value) {
            if (ROW_DIM == 0) {
                _values[row, col] = value;
            } else {
#pragma warning disable CS0162
                _values[row, col] = value;
#pragma warning restore CS0162
            }
        }

        float GetValueInternal(int col, int row) {
            // despite the vectors logic where horizontal coordinate X is written first,
            // in case of tabular data, we use rows first and columns second.
            // this can be changed by setting other values to the constants: ROW_DIM=1, COL_DIM=0
            return ROW_DIM == 0 ? _values[row, col] : _values[col, row];
        }

        int GetSize(int dimension) {
            return _values == null ? 0 : _values.GetLength(dimension);
        }

        public enum OutOfRangePolicy {
            DefaultValue,
            ClampIndex,
            LoopIndex,
            NotFound
        }

        public enum SeparatorType {
            Tab = '\t',
            Comma = ',',
            Semicolon = ';'
        }

        class D1NavDictionary : IReadOnlyDictionary<string, float> {
            readonly SimpleFloatTable _parent;
            readonly IReadOnlyDictionary<string, int> _dimNamesMap;

            // determines if we iterate by row or column
            readonly int _dimesion;

            // for row dimension it's a rowIdx, otherwise it's a columnIdx
            readonly int _otherDimIndex;

            public D1NavDictionary(SimpleFloatTable parent, int dimension, int lineIndex) {
                _parent = parent;
                if (dimension == ROW_DIM)
                    _dimNamesMap = _parent._cachedRowIds;
                else
                    _dimNamesMap = _parent._cachedColumnIds;

                _dimesion = dimension;
                _otherDimIndex = lineIndex;
            }

            float GetValue(int thisDimIndex) {
                // see also SimpleFloatTable.GetValueInternal for wider explanation
                return _dimesion == 0
                    ? _parent._values[thisDimIndex, _otherDimIndex]
                    : _parent._values[_otherDimIndex, thisDimIndex];
            }

            public float this[string key] { get => GetValue(_dimNamesMap[key]); }

            public IEnumerable<string> Keys { get => _dimNamesMap.Keys; }
            public IEnumerable<float> Values {
                get {
                    var size = Count;
                    for (int i = 0; i < size; i++) {
                        yield return GetValue(i);
                    }
                }
            }

            public int Count { get => _parent.GetSize(_dimesion); }

            public bool ContainsKey(string key) {
                return _dimNamesMap.ContainsKey(key);
            }

            public bool TryGetValue(string key, out float value) {
                var result = _dimNamesMap.TryGetValue(key, out var thisDimIndex);
                if (result) {
                    value = GetValue(thisDimIndex);
                } else {
                    value = 0;
                }
                return result;
            }

            public IEnumerator<KeyValuePair<string, float>> GetEnumerator() {
                foreach (var kvp in _parent._cachedColumnIds) {
                    yield return new(kvp.Key, GetValue(kvp.Value));
                }
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }
        }
    }
}
