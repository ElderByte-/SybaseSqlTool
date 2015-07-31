using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sybase.Data.AseClient;

namespace SybaseSqlTool.Model
{
    public class SybaseDatabaseConnection : IDatabaseConnection
    {
        private AseConnection _connection;

        public SybaseDatabaseConnection(string connectionString)
        {
            Connect(connectionString);
        }

        public DataTable ExecuteSql(string sql)
        {
            var dataSet = new DataSet();

            var command = new AseCommand(sql, _connection)
            {
                CommandTimeout = 21600
            };

            var adapter = new AseDataAdapter(command);
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
            _connection = new AseConnection(connectionString);
            _connection.Open();
        }
    }
}
