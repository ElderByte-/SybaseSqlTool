using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SybaseSqlTool.Model
{
    public interface IDatabaseConnection : IDisposable
    {
        bool IsConnected { get; }

        DataTable ExecuteSql(string sql);
    }
}
