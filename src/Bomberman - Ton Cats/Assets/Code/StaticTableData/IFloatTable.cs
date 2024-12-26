using System.Collections.Generic;
using MappedSpan = System.Collections.Generic.IReadOnlyDictionary<string, float>;

namespace StaticTableData {
    public interface IFloatTable {
        int ColumnCount { get; }
        int RowCount { get; }
        NavigationType NavType { get; }

        float GetValue(int columnIdx, int rowIdx);

        #region NAMED_COLUMNS
        bool HasColumn(string columnName);
        float GetValue(string columnName, int rowIdx);

        IReadOnlyCollection<string> GetColumnNames();
        IReadOnlyCollection<string> GetRowNames();

        bool TryGetColumn(string name, out int columnIdx);
        bool TryGetRow(string name, out int rowIdx);
        bool TryGetMappedRow(string rowName, out MappedSpan dict);
        bool TryGetMappedRow(int rowIndex, out MappedSpan dict);
        bool TryGetMappedColumn(string columnName, out MappedSpan dict);
        bool TryGetMappedColumn(int columnIndex, out MappedSpan dict);
        bool TryGetValue(string columnName, string rowName, out float value);
        #endregion NAMED_COLUMNS

        [System.Flags]
        public enum NavigationType {
            OnlyIndices = 0b00,
            NamedColumns = 0b01,
            NamedRows = 0b10,
        }
    }
}
