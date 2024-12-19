using System.Collections.Generic;

namespace Static_table_data {
    public interface IFloatTable {
        int ColumnCount { get; }
        int RowCount { get; }

        float GetValue(int columnIdx, int rowIdx);

        #region NAMED_COLUMNS
        bool HasColumn(string columnName);
        IReadOnlyCollection<string> GetColumnNames();
        #endregion NAMED_COLUMNS
    }
}
