using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace SybaseSqlTool.Model
{
    /// <summary>
    /// CharSet=UTF8;
    /// </summary>
    public class MsSQLDatabaseConnection : IDatabaseConnection
    {
        private SqlConnection _connection;

        public MsSQLDatabaseConnection(string connectionString)
        {
            Connect(connectionString);
        }

        public DataTable ExecuteSql(string sql)
        {
            var dataSet = new DataSet();

            var command = new SqlCommand(sql, _connection)
            {
                CommandTimeout = 21600
            };

            var adapter = new SqlDataAdapter(command);
            adapter.Fill(dataSet, "Result");

            return dataSet.Tables["Result"];
        }

        public bool IsConnected => _connection != null && _connection.State == ConnectionState.Open;


        public void Dispose()
        {
            _connection?.Close();
        }


        private void Connect(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }
    }
}
