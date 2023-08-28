using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace DataAccess.SQLiteProvider
{
    /// <summary>
    /// SQLite连接帮助类
    /// </summary>
    public class SQLiteConnectionHolder
    {
        internal SqliteConnection _connection;
        internal SQLiteConnection connection;
        private bool _opened = false;

        public SqliteConnection Connection
        {
            get { return _connection; }
        }

        public SQLiteConnection SqliteConnection
        {
            get { return connection; }
            set { connection = value; }
        }

        public SQLiteConnectionHolder() { }

        internal SQLiteConnectionHolder(string sqliteFileFullPath)
        {
            try
            {
                string ConnectionString = string.Empty;
                if(!File.Exists(sqliteFileFullPath)) 
                {
                    throw new Exception("database file is not found");
                }
                ConnectionString = $"Data Source={sqliteFileFullPath}";
                if(_connection==null)
                   _connection = new SqliteConnection(ConnectionString);
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Error_Connection_String", "connectionString", e);
            }
        }

        public void InstantiationConnection(string sqliteFileFullPath) 
        {
            try
            {
                string ConnectionString = string.Empty;
                if (!File.Exists(sqliteFileFullPath))
                {
                    throw new Exception("database file is not found");
                }
                ConnectionString = $"Data Source={sqliteFileFullPath}";
                if (connection == null)
                    connection = new SQLiteConnection(ConnectionString);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Error_Connection_String", "connectionString", e);
            }
        }

        public void Close()
        {
            if (_connection.State == ConnectionState.Closed)
                return;
            _connection.Close();
        }

    }
}
