using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Archimedes.Patterns.Utils;
using Archimedes.Patterns.WPF.Commands;
using Archimedes.Patterns.WPF.ViewModels;
using SybaseSqlTool.Model;

namespace SybaseSqlTool
{
    public class MainViewModel : ViewModelWPF
    {
        private IDatabaseConnection _connection;
        private DataTable _resultData;


        public MainViewModel()
        {
            DatabaseType = DatabaseType.Sybase;

            //new CharsetFixer().FixCharset();

        }

        #region Viewmodel Properties

        public string ConnectionString { get; set; }

        public string QueryString { get; set; }

        public DatabaseType DatabaseType { get; set; }

        public IEnumerable<DatabaseType> AvailableDatabaseTypes => Enum.GetValues(typeof (DatabaseType)).Cast<DatabaseType>();

        public ICommand ConnectCommand
        {
            get { return new RelayCommand(x =>
            {
                try
                {
                    Connect(ConnectionString);
                }
                catch (Exception e)
                {
                    var errorMsg = ExceptionUtil.ComposeSingleLineMessage(e, true);
                    MessageBox.Show(errorMsg, "Failed to connect!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, x => !string.IsNullOrEmpty(ConnectionString)); }
        }


        public ICommand ExecuteCommand
        {
            get
            {
                return new RelayCommand(x =>
                {
                    try
                    {
                        ExecuteSql(QueryString);
                    }
                    catch (Exception e)
                    {
                        var errorMsg = ExceptionUtil.ComposeSingleLineMessage(e, true);
                        MessageBox.Show(errorMsg, "Failed to execute sql!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                },
                x => !string.IsNullOrEmpty(QueryString) && _connection != null && _connection.IsConnected);
            }
        }

        public DataTable ResultData
        {
            get { return _resultData; }
            private set
            {
                _resultData = value;
                OnPropertyChanged(()=> ResultData);
            }
        }

        #endregion

        private void Connect(string connectionString)
        {
            switch (DatabaseType)
            {
                    case DatabaseType.Sybase:
                    _connection = new SybaseDatabaseConnection(connectionString);
                    break;

                    case DatabaseType.MsSql:
                    _connection = new MsSQLDatabaseConnection(connectionString);
                    break;

                default:
                    throw new NotSupportedException("Unknown Database Type!");
            }
        }

        private void ExecuteSql(string sql)
        {
            var dataTable = _connection.ExecuteSql(sql);
            ResultData = dataTable;
        }
    }
}
