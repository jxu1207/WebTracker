using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebTracker.ModelAgent
{
    internal class DbUtils
    {
        public static string GetStringValue(MySqlDataReader reader, string columnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(columnName)) ? string.Empty : reader.GetString(reader.GetOrdinal(columnName));
        }

        public static DateTime GetDateTimeValue(MySqlDataReader reader, string columnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(columnName)) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal(columnName));
        }

        public static int GetIntValue(MySqlDataReader reader, string columnName, int def = 0)
        {
            return reader.IsDBNull(reader.GetOrdinal(columnName)) ? def : reader.GetInt32(reader.GetOrdinal(columnName));
        }
    }
}
