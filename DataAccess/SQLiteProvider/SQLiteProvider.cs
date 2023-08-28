using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace DataAccess.SQLiteProvider
{
    /// <summary>
    /// SQLite数据提供类
    /// </summary>
    public class SQLiteProvider
    {
        /// <summary>
        /// SQLite数据库连接类
        /// </summary>
        private SqliteConnection _connection;

        /// <summary>
        /// SQLite数据库连接类
        /// </summary>
        private SQLiteConnection connection;

        /// <summary>
        /// SQLite连接提供类
        /// </summary>
        private SQLiteConnectionHolder connectionHolder;

        /// <summary>
        /// SQL文件路径
        /// </summary>
        private string SqliteFileFullPath;

        public SQLiteProvider(string sqliteFileFullPath) 
        {
            //message = string.Empty;
            SqliteFileFullPath = sqliteFileFullPath;
            //CreateConnection(out message);
        }

        public SqliteParameter CreateInputParam(string paramName, SqliteType dbType, object objValue, int size)
        {
            SqliteParameter parameter;
            if (size > 0)
                parameter = new SqliteParameter(paramName, dbType, size);
            else
                parameter = new SqliteParameter(paramName, dbType);

            if (objValue == null)
            {
                parameter.IsNullable = true;
                parameter.Value = DBNull.Value;
                return parameter;
            }
            if (objValue is string)
            {
                if (string.IsNullOrEmpty(Convert.ToString(objValue)))
                {
                    parameter.IsNullable = true;
                    parameter.Value = DBNull.Value;
                    return parameter;
                }
            }
            parameter.Value = objValue;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        private void CreateConnection(out string message) 
        {
            message = string.Empty;
            try
            {
                var connectionHolder = new SQLiteConnectionHolder(SqliteFileFullPath);
                _connection = connectionHolder.Connection;
            }
            catch (Exception exp)
            {
                message = exp.Message;
            }
        }

        private string GetNullableString(SqliteDataReader reader, int col)
        {
            if (!reader.IsDBNull(col))
            {
                return reader.GetString(col);
            }
            return null;
        }

        private int GetNullableInt(SqliteDataReader reader, int col)
        {
            if (!reader.IsDBNull(col))
            {
                return reader.GetInt32(col);
            }
            return -1;
        }

        private DateTime GetNullableDateTime(SqliteDataReader reader, int col)
        {
            if (!reader.IsDBNull(col))
            {
                return reader.GetDateTime(col);
            }
            return Convert.ToDateTime("0000-01-01 00:00:00");
        }

        public int CreateTransDate(ArrayList sqlStrs, out string message)
        {
            message = "";
            int status = 0;
            try
            {
                CreateConnection(out message);
                SqliteTransaction myTrans = _connection.BeginTransaction();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = _connection;
                    command.Transaction = myTrans;

                    for (int i = 0; i < sqlStrs.Count; i++)
                    {
                        command.CommandText = sqlStrs[i].ToString();
                        int rows = command.ExecuteNonQuery();

                        if (rows > 1)
                        {
                            myTrans.Rollback();
                            message = "106:数据库事务处理出错!";
                            return 106;
                        }
                    }

                    myTrans.Commit();
                    command.Dispose();
                    return status;
                }
                catch (Exception ex)
                {
                    myTrans.Rollback();
                    message = "107:数据库事务处理异常!" + ex.Message;
                    return 107;
                }
                finally
                {
                    if (_connection != null)
                    {
                        _connection.Close();
                        _connection = null;
                    }
                }
            }
            catch (Exception ex)
            {
                message = "108:与数据库连接错误!" + ex.Message;
                return 108;
            }
        }

        /// <summary>
        /// 创建或修改表数据:对于单条记录
        /// </summary>
        /// <param name="sqlStr">表名或存储过程</param>
        /// <param name="parameters">Parameter 数组</param>
        /// <param name="message">返回文本</param>
        /// <returns>0：成功 !0失败</returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType, SqliteParameter[] parameters, out string message)
        {
            try
            {
                SqliteCommand command = null;
                try
                {
                    CreateConnection(out message);
                    command = new SqliteCommand();
                    command.Connection = _connection;
                    command.CommandText = commandText;
                    command.CommandType = commandType;
                    if (parameters != null) AddParameter(command, parameters);

                    try
                    {
                        int rowCount = command.ExecuteNonQuery();
                        if (rowCount > 1)
                        {
                            message = "数据库操作错误!";
                            return 903;
                        }
                        else
                        {
                            message = ""; return 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        return 902;
                    }
                }
                finally
                {
                    command.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                message = "连接失败!" + ex.Message;
                return 901;
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Close();
                    _connection = null;
                }
            }
        }

        public int UpdateData(string sqlStrs, out string message)
        {
            message = "";
            int status = 0;
            try
            {
                CreateConnection(out message);
                SqliteTransaction myTrans = _connection.BeginTransaction();
                try
                {
                    SqliteCommand command = new SqliteCommand(sqlStrs, _connection);
                    command.Transaction = myTrans;
                    int rows = command.ExecuteNonQuery();
                    //if (rows == 0)
                    //{
                    //    message = "1:未找到相关数据!";
                    //    return 1;
                    //}
                    //if (rows > 1)
                    //{
                    //    message = "2:有多条相关数据!";
                    //    return 2;
                    //}
                    myTrans.Commit();
                    command.Dispose();
                    return status;
                }
                catch (Exception ex)
                {
                    myTrans.Rollback();
                    message = "3:创建信息异常!" + ex.Message;
                    return 3;
                }
                finally
                {
                    if (_connection != null)
                    {
                        _connection.Close();
                        _connection = null;
                    }
                }
            }
            catch (Exception ex)
            {
                message = "4:与WMS连接错误!" + ex.Message;
                return 4;
            }
        }

        public int UpdateDataOrder(string sqlStrs, out string message)
        {
            message = "";
            int status = 0;
            try
            {
                CreateConnection(out message);
                SqliteTransaction myTrans = _connection.BeginTransaction();
                try
                {
                    SqliteCommand command = new SqliteCommand(sqlStrs, _connection);
                    command.Transaction = myTrans;
                    int rows = command.ExecuteNonQuery();

                    myTrans.Commit();
                    command.Dispose();
                    return status;
                }
                catch (Exception ex)
                {
                    myTrans.Rollback();
                    message = "3:创建信息异常!" + ex.Message;
                    return 3;
                }
                finally
                {
                    if (_connection != null)
                    {
                        _connection.Close();
                        _connection = null;
                    }
                }
            }
            catch (Exception ex)
            {
                message = "与数据库连接错误!" + ex.Message;
                return 4;
            }
        }

        /// <summary>修改数据
        /// 修改数据
        /// </summary>
        /// <param name="sqlStrs">SQL 语句</param>
        /// <param name="message">返回结果文本</param>
        /// <returns>0.成功；非0.失败</returns>
        public int UpdateTransData(ArrayList sqlStrs, out string message)
        {
            message = "";
            int status = 0;
            try
            {
                CreateConnection(out message);
                SqliteTransaction myTrans = _connection.BeginTransaction();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = _connection;
                    command.Transaction = myTrans;

                    for (int i = 0; i < sqlStrs.Count; i++)
                    {
                        command.CommandText = sqlStrs[i].ToString();
                        int rows = command.ExecuteNonQuery();

                        //if (rows != 1)
                        //{
                        //    myTrans.Rollback();
                        //    message = "206:数据库事务处理出错!";
                        //    return 206;
                        //}
                    }
                    myTrans.Commit();
                    command.Dispose();
                    return status;
                }
                catch (Exception ex)
                {
                    myTrans.Rollback();
                    message = "207:数据库事务处理异常!" + ex.Message;
                    return 207;
                }
                finally
                {
                    if (_connection != null)
                    {
                        _connection.Close();
                        _connection = null;
                        myTrans.Dispose();
                        myTrans = null;
                    }
                }
            }
            catch (Exception ex)
            {
                message = "208:与数据库连接错误!" + ex.Message;
                return 208;
            }
        }

        public DataRow GetDataByKey(string sqlStrs, out string message)
        {
            message = "";
            try
            {
                
                try
                {
                    connectionHolder = new SQLiteConnectionHolder();
                    connectionHolder.InstantiationConnection(SqliteFileFullPath);
                    SQLiteDataAdapter oraDap = new SQLiteDataAdapter(sqlStrs, connectionHolder.SqliteConnection);
                    DataSet ds = new DataSet();

                    try
                    {
                        oraDap.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        if (dt == null || dt.Rows.Count == 0)
                        {
                            message = "1:没有相关数据!";
                            return null;
                        }

                        if (dt.Rows.Count > 1)
                        {
                            message = "2:有重复数据!";
                            return dt.Rows[0];
                        }

                        return dt.Rows[0];
                    }
                    catch (Exception ex)
                    {
                        message = "3:读取数据失败!" + ex.Message;
                        return null;
                    }
                    finally
                    {
                        oraDap.Dispose();
                        ds.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    message = "4:查询异常!" + ex.Message;
                    return null;
                }
                finally
                {
                    if (connectionHolder.SqliteConnection != null)
                    {
                        connectionHolder.SqliteConnection.Close();
                        connectionHolder.SqliteConnection = null;
                    }
                }
            }
            catch (Exception ex)
            {
                message = "5:与WMS连接错误!" + ex.Message;
                return null;
            }
        }

        public DataTable GetDataTable(string sqlStrs, out string message)
        {
            message = "";
            try
            {
                try
                {
                    connectionHolder = new SQLiteConnectionHolder();
                    connectionHolder.InstantiationConnection(SqliteFileFullPath);
                    SQLiteDataAdapter oraDap = new SQLiteDataAdapter(sqlStrs, connectionHolder.SqliteConnection);
                    DataSet ds = new DataSet();

                    try
                    {
                        oraDap.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        if (dt == null)
                        {
                            message = "1:没有相关数据!";
                            return null;
                        }

                        if (dt.Rows.Count < 0)
                        {
                            message = "2:没有相关数据2!";
                            return null;
                        }

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        message = "3:读取数据失败!" + ex.Message;
                        return null;
                    }
                    finally
                    {
                        oraDap.Dispose();
                        ds.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    message = "4:查询异常!" + ex.Message;
                    return null;
                }
                finally
                {
                    if (connectionHolder.SqliteConnection != null)
                    {
                        connectionHolder.SqliteConnection.Close();
                        connectionHolder.SqliteConnection = null;
                    }
                }
            }
            catch (Exception ex)
            {
                message = "5:与WMS连接错误!" + ex.Message;
                return null;
            }
        }

        public DataTable ExecuteDataSet(string commandText, CommandType commandType, SQLiteParameter[] parameters, out string message)
        {
            DataTable dt = null;
            try
            {
                CreateConnection(out message);
                SQLiteCommand command = null;
                SQLiteConnection connection = null;
                try
                {
                    connectionHolder = new SQLiteConnectionHolder();
                    connectionHolder.InstantiationConnection(SqliteFileFullPath);
                    command = new SQLiteCommand();
                    command.Connection = connectionHolder.SqliteConnection;
                    command.CommandText = commandText;
                    command.CommandType = commandType;
                    AddParameterBySQLite(command, parameters);

                    SQLiteDataAdapter sqlDap = new SQLiteDataAdapter();
                    sqlDap.SelectCommand = command;

                    DataSet ds = new DataSet();
                    try
                    {
                        sqlDap.Fill(ds);
                        dt = ds.Tables[0];
                        message = "";
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                    }
                    finally
                    {
                        ds.Dispose();
                        sqlDap.Dispose();
                    }
                }
                finally
                {
                    if(parameters != null&& parameters.Length > 0)
                       command.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                message = "与数据库连接错误!" + ex.Message;
            }
            finally
            {
                if (connectionHolder.SqliteConnection != null)
                {
                    connectionHolder.SqliteConnection.Close();
                    connectionHolder.SqliteConnection = null;
                }
            }
            return dt;
        }


        /// <summary>
        /// 添加SQL参数
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private int AddParameter(SqliteCommand sqlCommand, SqliteParameter[] parameters)
        {
            int i = 1;
            if (parameters == null || parameters.Length <= 0)
                return i;
            foreach (SqliteParameter p in parameters)
                sqlCommand.Parameters.Add(p);
            return i;
        }

        /// <summary>
        /// 添加SQL参数
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private int AddParameterBySQLite(SQLiteCommand sqlCommand, SQLiteParameter[] parameters)
        {
            int i = 1;
            if (parameters == null || parameters.Length <= 0)
                return i;
            foreach (SQLiteParameter p in parameters)
                sqlCommand.Parameters.Add(p);
            return i;
        }
    }
}
