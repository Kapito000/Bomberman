using System.Collections.Generic;

namespace StaticTableData {
    public interface IFloatTable {
        int ColumnCount { get; }
        int RowCount { get; }
        NavigationType NavType { get; }

        float GetValue(int columnIdx, int rowIdx);

        #region NAMED_COLUMNS
        bool HasColumn(string columnName);
        IReadOnlyCollection<string> GetColumnNames();
        IReadOnlyCollection<string> GetRowNames();
        float GetValue(string columnName, int rowIdx);
        bool TryGetColumn(string name, out int columnIdx);
        bool TryGetRow(string name, out int rowIdx);
        #endregion NAMED_COLUMNS

        [System.Flags]
        public enum NavigationType {
            OnlyIndices = 0b00,
            NamedColumns = 0b01,
            NamedRows = 0b10,
        }
    }
}
