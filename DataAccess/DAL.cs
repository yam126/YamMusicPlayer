using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using DataAccess.SQLiteProvider;
using Microsoft.EntityFrameworkCore.Sqlite;
using SQLitePCL;
using System.Web;

namespace DataAccess.DAL
{
    /// <summary>
    ///  
    /// </summary>
    public partial class MP3FileInfo_DAL
    {
        /// <summary>
        /// 数据库连接类
        /// </summary>
        private SQLiteProvider.SQLiteProvider sqlPro;

        #region 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="SQLiteFullFilePath">SQLite文件的全路径</param>
        /// <param name="message">错误消息</param>
        public MP3FileInfo_DAL(string SQLiteFullFilePath)
        {
            sqlPro = new SQLiteProvider.SQLiteProvider(SQLiteFullFilePath);
        }
        #endregion

        #region 增删改

        #region 增加数据

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="message">错误消息</param>
        /// <returns>添加条数</returns>
        public int Insert(List<MP3FileInfo> lists, out string message)
        {
            int result = -1;
            message = string.Empty;
            CheckEmpty(ref lists);
            CheckMaxLength(ref lists, out message);
            if (!string.IsNullOrEmpty(message))
                return result;
            ArrayList sqls = this.MarkInsertSql(lists);
            result = sqlPro.UpdateTransData(sqls, out message);
            return result;
        }
        #endregion

        #region 删除数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误信息</param>
        /// <returns>0成功非0失败</returns>
        public int Delete(string SqlWhere,out string message) 
        {
            int result = -1;
            message = string.Empty;
            string sql = " delete from MP3FileInfo ";
            if(string.IsNullOrEmpty(SqlWhere))
            {
                message = "删除条件不能为空";
                return -1;
            }
            sql += " where " + SqlWhere;
            ArrayList sqls = new ArrayList();
            sqls.Add(sql);
            result = sqlPro.UpdateTransData(sqls, out message);
            return result;
        }
        #endregion

        #region 修改方法

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="SqlWhere">更新条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>修改条数</returns>
        public int Update(List<MP3FileInfo> lists, string SqlWhere, out string message)
        {
            int result = -1;
            message = string.Empty;
            CheckEmpty(ref lists);
            CheckMaxLength(ref lists, out message);
            if (!string.IsNullOrEmpty(message))
                return result;
            ArrayList sqls = this.MarkUpdateSql(lists, SqlWhere);
            result = sqlPro.UpdateTransData(sqls, out message);
            return result;
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="FieldValue">字段值</param>
        /// <param name="SqlWhere">更新条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>0成功，非0失败</returns>
        public int Update(string FieldName,string FieldValue,string SqlWhere, out string message) 
        {
            int result = -1;
            message = string.Empty;
            string sql = $" update MP3FileInfo set {FieldName}='{FieldValue}' where {SqlWhere}";
            ArrayList sqls = new ArrayList();
            sqls.Add(sql);
            result = sqlPro.UpdateTransData(sqls, out message);
            return result;
        }
        #endregion

        #region 查询方法
        /// <summary>
        /// 查询单表方法
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>查询结果</returns>
        public List<MP3FileInfo> Query(string SqlWhere, out string message)
        {
            message = string.Empty;
            List<MP3FileInfo> result = new List<MP3FileInfo>();
            string sql = $"select * from MP3FileInfo ";
            if (!string.IsNullOrEmpty(SqlWhere))
                sql += $" where {SqlWhere} ";
            DataTable dt = sqlPro.ExecuteDataSet(sql, CommandType.Text, null, out message);
            if (dt == null)
                return result;
            if (dt.Rows.Count == 0)
                return result;
            foreach (DataRow dr in dt.Rows)
            {
                MP3FileInfo model = new MP3FileInfo();
                this.ReadDataRow(ref model, dr);
                result.Add(model);
            }
            return result;
        }

        /// <summary>
        /// 分页查询方法
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="SortField">排序字段名</param>
        /// <param name="SortMethod">排序方法[ASC|DESC]</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="PageCount">总页数</param>
        /// <param name="message">错误消息</param>
        /// <returns>查询结果</returns>
        public List<MP3FileInfo> Query(string SqlWhere, string SortField, string SortMethod, int PageSize, int CurPage, out int PageCount, out string message)
        {
            message = string.Empty;
            PageCount = 0;
            List<MP3FileInfo> result = new List<MP3FileInfo>();
            //SqlParameter[] sqlparm = new SqlParameter[] {
            //    new SqlParameter("@StartRow",SqlDbType.Int,4){Value=((CurPage - 1) * PageSize + 1)},
            //    new SqlParameter("@EndRow",SqlDbType.Int,4){Value=(CurPage * PageSize)},
            //    new SqlParameter("@TotalNumber",SqlDbType.Int,4){Direction=ParameterDirection.Output},
            //    new SqlParameter("@SortMethod",SqlDbType.NVarChar){Value=SortMethod},
            //    new SqlParameter("@SortField",SqlDbType.NVarChar){Value=SortField},
            //    new SqlParameter("@SqlWhere",SqlDbType.NVarChar){Value=SqlWhere}
            //};
            //DataTable dt = sqlPro.ExecuteDataSet("Query_MP3FileInfo_Page", CommandType.StoredProcedure, sqlparm, out message);
            //if (dt == null)
            //    return result;
            //if (dt.Rows.Count == 0)
            //    return result;
            //PageCount = Convert.ToInt32(sqlparm[2].Value);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    MP3FileInfo model = new MP3FileInfo();
            //    this.ReadDataRow(ref model, dr);
            //    result.Add(model);
            //}
            return result;
        }
        #endregion

        #endregion

        #region 基础方法

        /// <summary>
        /// 读取数据行到model
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="dr">数据行</param>
        private void ReadDataRow(ref MP3FileInfo model, DataRow dr)
        {
            model.SongListId = Convert.IsDBNull(dr["SongListId"]) ? 0 : Convert.ToInt64(dr["SongListId"]);
            //
            model.FileId = Convert.IsDBNull(dr["FileId"]) ? 0 : Convert.ToInt64(dr["FileId"]);
            //
            model.FileName = Convert.IsDBNull(dr["FileName"]) ? string.Empty : Convert.ToString(dr["FileName"]).Trim();
            //
            model.FileSize = Convert.IsDBNull(dr["FileSize"]) ? 0 : Convert.ToDouble(dr["FileSize"]);
            //
            model.CreateTime = Convert.IsDBNull(dr["CreateTime"]) ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(dr["CreateTime"]);
            //
            model.Cover = Convert.IsDBNull(dr["Cover"]) ? string.Empty : Convert.ToString(dr["Cover"]).Trim();
            //
            model.Title = Convert.IsDBNull(dr["Title"]) ? string.Empty : Convert.ToString(dr["Title"]).Trim();
            //
            model.duration = Convert.IsDBNull(dr["Duration"]) ? 0 : Convert.ToDouble(dr["Duration"]);
            //
            model.Artist = Convert.IsDBNull(dr["Artist"]) ? string.Empty : Convert.ToString(dr["Artist"]).Trim();
            //
            model.Album = Convert.IsDBNull(dr["Album"]) ? string.Empty : Convert.ToString(dr["Album"]).Trim();
            //
            model.Year = Convert.IsDBNull(dr["Year"]) ? string.Empty : Convert.ToString(dr["Year"]).Trim();
            //
            model.Comment = Convert.IsDBNull(dr["Comment"]) ? string.Empty : Convert.ToString(dr["Comment"]).Trim();
            //
            model.reserved1 = Convert.IsDBNull(dr["reserved1"]) ? string.Empty : Convert.ToString(dr["reserved1"]).Trim();
            //
            model.reserved2 = Convert.IsDBNull(dr["reserved2"]) ? string.Empty : Convert.ToString(dr["reserved2"]).Trim();
            //
            model.reserved3 = Convert.IsDBNull(dr["reserved3"]) ? string.Empty : Convert.ToString(dr["reserved3"]).Trim();
            //
            model.FileFullPath = Convert.IsDBNull(dr["FileFullPath"]) ? string.Empty : Convert.ToString(dr["FileFullPath"]).Trim();
            //
            model.LyricFilePath = Convert.IsDBNull(dr["LyricFilePath"]) ? string.Empty : Convert.ToString(dr["LyricFilePath"]).Trim();
        }

        ///<summary>
        ///检查是否空值
        ///</summary>
        private void CheckEmpty(ref List<MP3FileInfo> lists)
        {
            for (int i = 0; i < lists.Count; i++)
            {
                //
                lists[i].SongListId = lists[i].SongListId == null ? Convert.ToInt64(0) : Convert.ToInt64(lists[i].SongListId);
                //
                lists[i].FileId = lists[i].FileId == null ? Convert.ToInt64(0) : Convert.ToInt64(lists[i].FileId);
                //
                lists[i].FileName = string.IsNullOrEmpty(lists[i].FileName) ? string.Empty : Convert.ToString(lists[i].FileName).Trim();
                //
                lists[i].FileSize = lists[i].FileSize == null ? Convert.ToDouble(0) : Convert.ToDouble(lists[i].FileSize);
                //
                lists[i].CreateTime = lists[i].CreateTime == null ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(lists[i].CreateTime);
                //
                lists[i].Cover = string.IsNullOrEmpty(lists[i].Cover) ? string.Empty : Convert.ToString(lists[i].Cover).Trim();
                //
                lists[i].Title = string.IsNullOrEmpty(lists[i].Title) ? string.Empty : Convert.ToString(lists[i].Title).Trim();
                //
                lists[i].duration = lists[i].duration == null ? Convert.ToDouble(0) : Convert.ToDouble(lists[i].duration);
                //
                lists[i].Artist = string.IsNullOrEmpty(lists[i].Artist) ? string.Empty : Convert.ToString(lists[i].Artist).Trim();
                //
                lists[i].Album = string.IsNullOrEmpty(lists[i].Album) ? string.Empty : Convert.ToString(lists[i].Album).Trim();
                //
                lists[i].Year = string.IsNullOrEmpty(lists[i].Year) ? string.Empty : Convert.ToString(lists[i].Year).Trim();
                //
                lists[i].Comment = string.IsNullOrEmpty(lists[i].Comment) ? string.Empty : Convert.ToString(lists[i].Comment).Trim();
                //
                lists[i].reserved1 = lists[i].reserved1==null ? null : Convert.ToString(lists[i].reserved1).Trim();
                //
                lists[i].reserved2 = lists[i].reserved2==null ? null : Convert.ToString(lists[i].reserved2).Trim();
                //
                lists[i].reserved3 = lists[i].reserved3==null ? null : Convert.ToString(lists[i].reserved3).Trim();
                //
                lists[i].FileFullPath = lists[i].FileFullPath == null ? null : Convert.ToString(lists[i].FileFullPath).Trim();
                //
                lists[i].LyricFilePath = lists[i].LyricFilePath == null ? null : Convert.ToString(lists[i].LyricFilePath).Trim();
            }
        }

        ///<summary>
        ///检查是否超过长度
        ///</summary>
        ///<param name="lists">数据集</param>
        ///<param name="message">错误消息</param>
        private void CheckMaxLength(ref List<MP3FileInfo> lists, out string message)
        {
            #region 声明变量

            //错误消息
            message = string.Empty;

            //超过的长度
            int OutLength = 0;
            #endregion

            #region 循环验证长度
            for (int i = 0; i < lists.Count; i++)
            {
                if (!string.IsNullOrEmpty(lists[i].FileName))
                {
                    if (lists[i].FileName.Length > 200)
                    {
                        OutLength = lists[i].FileName.Length - 200;
                        message += "字段名[FileName]描述[]超长、字段最长[200]实际" + lists[i].FileName.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Title))
                {
                    if (lists[i].Title.Length > 300)
                    {
                        OutLength = lists[i].Title.Length - 300;
                        message += "字段名[Title]描述[]超长、字段最长[300]实际" + lists[i].Title.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Artist))
                {
                    if (lists[i].Artist.Length > 200)
                    {
                        OutLength = lists[i].Artist.Length - 200;
                        message += "字段名[Artist]描述[]超长、字段最长[200]实际" + lists[i].Artist.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Album))
                {
                    if (lists[i].Album.Length > 200)
                    {
                        OutLength = lists[i].Album.Length - 200;
                        message += "字段名[Album]描述[]超长、字段最长[200]实际" + lists[i].Album.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Year))
                {
                    if (lists[i].Year.Length > 80)
                    {
                        OutLength = lists[i].Year.Length - 80;
                        message += "字段名[Year]描述[]超长、字段最长[80]实际" + lists[i].Year.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Comment))
                {
                    if (lists[i].Comment.Length > 200)
                    {
                        OutLength = lists[i].Comment.Length - 200;
                        message += "字段名[Comment]描述[]超长、字段最长[200]实际" + lists[i].Comment.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].reserved1))
                {
                    if (lists[i].reserved1.Length > 255)
                    {
                        OutLength = lists[i].reserved1.Length - 255;
                        message += "字段名[reserved1]描述[]超长、字段最长[255]实际" + lists[i].reserved1.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].reserved2))
                {
                    if (lists[i].reserved2.Length > 255)
                    {
                        OutLength = lists[i].reserved2.Length - 255;
                        message += "字段名[reserved2]描述[]超长、字段最长[255]实际" + lists[i].reserved2.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].reserved3))
                {
                    if (lists[i].reserved3.Length > 255)
                    {
                        OutLength = lists[i].reserved3.Length - 255;
                        message += "字段名[reserved3]描述[]超长、字段最长[255]实际" + lists[i].reserved3.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].Cover))
                {
                    if (lists[i].Cover.Length > 2900)
                    {
                        OutLength = lists[i].Cover.Length - 2900;
                        message += "字段名[Cover]描述[]超长、字段最长[2900]实际" + lists[i].Cover.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].FileFullPath))
                {
                    if (lists[i].FileFullPath.Length > 2900)
                    {
                        OutLength = lists[i].FileFullPath.Length - 2900;
                        message += "字段名[FileFullPath]描述[]超长、字段最长[2900]实际" + lists[i].FileFullPath.Length + "超过长度" + OutLength + ",";
                    }
                }
                if (!string.IsNullOrEmpty(lists[i].LyricFilePath))
                {
                    if (lists[i].LyricFilePath.Length > 2800)
                    {
                        OutLength = lists[i].LyricFilePath.Length - 2800;
                        message += "字段名[LyricFilePath]描述[]超长、字段最长[2900]实际" + lists[i].LyricFilePath.Length + "超过长度" + OutLength + ",";
                    }
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(message)) message = message.Substring(0, message.Length - 1);
        }

        ///<summary>
        ///生成插入Sql语句
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<returns>插入Sql语句字符串数组</returns>
        private ArrayList MarkInsertSql(List<MP3FileInfo> lists)
        {
            ArrayList result = new ArrayList();
            foreach (MP3FileInfo model in lists)
            {
                #region 拼写Sql语句
                string Sql = "insert into MP3FileInfo(";
                Sql += "FileId,";
                Sql += "FileName,";
                Sql += "FileSize,";
                Sql += "SongListId,";
                Sql += "CreateTime,";
                Sql += "Cover,";
                Sql += "Title,";
                Sql += "Duration,";
                Sql += "Artist,";
                Sql += "Album,";
                Sql += "Year,";
                Sql += "Comment,";
                Sql += "reserved1,";
                Sql += "reserved2,";
                Sql += "reserved3,";
                Sql += "FileFullPath,";
                Sql += "LyricFilePath";
                Sql += ") values(";
                Sql += "'" + FilteSQLStr(model.FileId) + "',";
                Sql += "'" + FilteSQLStr(model.FileName) + "',";
                Sql += "'" + FilteSQLStr(model.FileSize) + "',";
                Sql += "'" + FilteSQLStr(model.SongListId) + "',";
                Sql += "'" + model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                Sql += "'" + FilteSQLStr(model.Cover) + "',";
                Sql += "'" + FilteSQLStr(model.Title) + "',";
                Sql += "'" + FilteSQLStr(model.duration) + "',";
                Sql += "'" + FilteSQLStr(model.Artist) + "',";
                Sql += "'" + FilteSQLStr(model.Album) + "',";
                Sql += "'" + FilteSQLStr(model.Year) + "',";
                Sql += "'" + FilteSQLStr(model.Comment) + "',";
                Sql += "'" + FilteSQLStr(model.reserved1) + "',";
                Sql += "'" + FilteSQLStr(model.reserved2) + "',";
                Sql += "'" + FilteSQLStr(model.reserved3) + "',";
                Sql += "'" + FilteSQLStr(model.FileFullPath) + "',";
                Sql += "'" + FilteSQLStr(model.LyricFilePath) + "'";
                Sql += ")";
                #endregion
                result.Add(Sql);
            }
            return result;
        }

        ///<summary>
        ///生成更新Sql语句
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<param name="SqlWhere">更新条件</param>
        ///<returns>更新Sql语句字符串数组</returns>
        private ArrayList MarkUpdateSql(List<MP3FileInfo> lists, string SqlWhere)
        {
            ArrayList result = new ArrayList();
            foreach (MP3FileInfo model in lists)
            {
                #region 拼写Sql语句
                string Sql = "update MP3FileInfo set ";
                Sql += "FileId='" + FilteSQLStr(model.FileId) + "',";
                Sql += "FileName='" + FilteSQLStr(model.FileName) + "',";
                Sql += "FileSize='" + FilteSQLStr(model.FileSize) + "',";
                Sql += "SongListId='" + FilteSQLStr(model.SongListId) + "',";
                Sql += "CreateTime='" + model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                Sql += "Cover='" + FilteSQLStr(model.Cover) + "',";
                Sql += "Title='" + FilteSQLStr(model.Title) + "',";
                Sql += "Duration='" + FilteSQLStr(model.duration) + "',";
                Sql += "Artist='" + FilteSQLStr(model.Artist) + "',";
                Sql += "Album='" + FilteSQLStr(model.Album) + "',";
                Sql += "Year='" + FilteSQLStr(model.Year) + "',";
                Sql += "Comment='" + FilteSQLStr(model.Comment) + "',";
                Sql += "reserved1='" + FilteSQLStr(model.reserved1) + "',";
                Sql += "reserved2='" + FilteSQLStr(model.reserved2) + "',";
                Sql += "reserved3='" + FilteSQLStr(model.reserved3) + "',";
                Sql += "FileFullPath='" + FilteSQLStr(model.FileFullPath) + "',";
                Sql += "LyricFilePath='" + FilteSQLStr(model.LyricFilePath) + "'";
                if (!string.IsNullOrEmpty(SqlWhere))
                    Sql += " Where " + SqlWhere;
                #endregion
                result.Add(Sql);
            }
            return result;
        }
        #endregion

        #region 封装方法
        /// <summary>
        /// 过滤不安全的字符串
        /// </summary>
        /// <param name="Str">要过滤的值</param>
        /// <returns>返回结果</returns>
        private static string FilteSQLStr(object str)
        {
            if (str == null)
                return string.Empty;
            if (IsNumeric(str))
                return Convert.ToString(str);
            string Str = Convert.ToString(str);
            if (!string.IsNullOrEmpty(Str))
            {
                Str = Str.Replace("'", "");
                Str = Str.Replace("\"", "");
                Str = Str.Replace("&", "&amp");
                Str = Str.Replace("<", "&lt");
                Str = Str.Replace(">", "&gt");

                Str = Str.Replace("delete", "");
                Str = Str.Replace("update", "");
                Str = Str.Replace("insert", "");
            }
            return Str;
        }

        /// <summary>
        /// 判断object是否数字
        /// </summary>
        /// <param name="AObject">要判断的Object</param>
        /// <returns>是否数字</returns>       
        public static bool IsNumeric(object AObject)
        {
            return AObject is sbyte || AObject is byte ||
                AObject is short || AObject is ushort ||
                AObject is int || AObject is uint ||
                AObject is long || AObject is ulong ||
                AObject is double || AObject is char ||
                AObject is decimal || AObject is float ||
                AObject is double;
        }
        #endregion
    }

    public partial class SongList_DAL
    {
        /// <summary>
        /// 数据库连接类
        /// </summary>
        private SQLiteProvider.SQLiteProvider sqlPro;

        #region 构造函数
        /// <summary>
        /// 默认构造函数WebService调用
        /// </summary>
        public SongList_DAL(string SQLiteFullFilePath)
        {
            sqlPro = new SQLiteProvider.SQLiteProvider(SQLiteFullFilePath);
        }
        #endregion

        #region 增删改

        #region 增加数据

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="message">错误消息</param>
        /// <returns>添加条数</returns>
        public int Insert(List<SongList> lists, out string message)
        {
            int result = -1;
            message = string.Empty;
            CheckEmpty(ref lists);
            CheckMaxLength(ref lists, out message);
            if (!string.IsNullOrEmpty(message))
                return result;
            ArrayList sqls = this.MarkInsertSql(lists);
            result = sqlPro.UpdateTransData(sqls, out message);
            return result;
        }
        #endregion

        #region 删除数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误信息</param>
        /// <returns>0成功非0失败</returns>
        public int Delete(string SqlWhere, out string message)
        {
            int result = -1;
            message = string.Empty;
            string sql = " delete from SongList ";
            if (string.IsNullOrEmpty(SqlWhere))
            {
                message = "删除条件不能为空";
                return -1;
            }
            sql += " where " + SqlWhere;
            ArrayList sqls = new ArrayList();
            sqls.Add(sql);
            result = sqlPro.UpdateTransData(sqls, out message);
            return result;
        }
        #endregion

        #region 修改方法

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="lists">批量数据</param>
        /// <param name="SqlWhere">更新条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>修改条数</returns>
        public int Update(List<SongList> lists, string SqlWhere, out string message)
        {
            int result = -1;
            message = string.Empty;
            CheckEmpty(ref lists);
            CheckMaxLength(ref lists, out message);
            if (!string.IsNullOrEmpty(message))
                return result;
            ArrayList sqls = this.MarkUpdateSql(lists, SqlWhere);
            result = sqlPro.UpdateTransData(sqls, out message);
            return result;
        }

        /// <summary>
        /// 设置默认歌单
        /// </summary>
        /// <param name="SongListId">歌单编号</param>
        /// <param name="message">错误消息</param>
        /// <returns>0成功,非0失败</returns>
        public int SetDefault(long SongListId,out string message) 
        {
            int result = -1;
            message = string.Empty;
            ArrayList sqls = new ArrayList();
            sqls.Add($" update SongList set IsDefault=1 where SongListId='{SongListId}' ");
            sqls.Add($" update SongList set IsDefault=0 where SongListId<>'{SongListId}' ");
            result = sqlPro.UpdateTransData(sqls, out message);
            return result;
        }
        #endregion

        #region 查询方法
        /// <summary>
        /// 查询单表方法
        /// </summary>
        /// <param name="SqlWhere">查询条件</param>
        /// <param name="message">错误消息</param>
        /// <returns>查询结果</returns>
        public List<SongList> Query(string SqlWhere, out string message)
        {
            message = string.Empty;
            List<SongList> result = new List<SongList>();
            string sql = $"select * from SongList ";
            if (!string.IsNullOrEmpty(SqlWhere))
                sql += $" where {SqlWhere} ";
            DataTable dt = sqlPro.ExecuteDataSet(sql, CommandType.Text, null, out message);
            if (dt == null)
                return result;
            if (dt.Rows.Count == 0)
                return result;
            foreach (DataRow dr in dt.Rows)
            {
                SongList model = new SongList();
                this.ReadDataRow(ref model, dr);
                result.Add(model);
            }
            return result;
        }
        #endregion

        #endregion

        #region 基础方法

        /// <summary>
        /// 读取数据行到model
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="dr">数据行</param>
        private void ReadDataRow(ref SongList model, DataRow dr)
        {
            //
            model.SongListId = Convert.IsDBNull(dr["SongListId"]) ? 0 : Convert.ToInt64(dr["SongListId"]);
            //
            model.SongListName = Convert.IsDBNull(dr["SongListName"]) ? string.Empty : Convert.ToString(dr["SongListName"]).Trim();
            //
            model.CreateTime = Convert.IsDBNull(dr["CreateTime"]) ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(dr["CreateTime"]);
            //
            model.IsDefault = Convert.IsDBNull(dr["IsDefault"]) ? 0 : Convert.ToInt32(dr["IsDefault"]);
        }

        ///<summary>
        ///检查是否空值
        ///</summary>
        private void CheckEmpty(ref List<SongList> lists)
        {
            for (int i = 0; i < lists.Count; i++)
            {
                //
                lists[i].SongListId = lists[i].SongListId == null ? Convert.ToInt64(0) : Convert.ToInt64(lists[i].SongListId);
                //
                lists[i].SongListName = string.IsNullOrEmpty(lists[i].SongListName) ? string.Empty : Convert.ToString(lists[i].SongListName).Trim();
                //
                lists[i].CreateTime = lists[i].CreateTime == null ? (DateTime)SqlDateTime.MinValue : Convert.ToDateTime(lists[i].CreateTime.GetValueOrDefault());
                //
                lists[i].IsDefault = lists[i].IsDefault == null ? 0 : Convert.ToInt32(lists[i].IsDefault.GetValueOrDefault());
            }
        }

        ///<summary>
        ///检查是否超过长度
        ///</summary>
        ///<param name="lists">数据集</param>
        ///<param name="message">错误消息</param>
        private void CheckMaxLength(ref List<SongList> lists, out string message)
        {
            #region 声明变量

            //错误消息
            message = string.Empty;

            //超过的长度
            int OutLength = 0;
            #endregion

            #region 循环验证长度
            for (int i = 0; i < lists.Count; i++)
            {
                if (!string.IsNullOrEmpty(lists[i].SongListName))
                {
                    if (lists[i].SongListName.Length > 200)
                    {
                        OutLength = lists[i].SongListName.Length - 200;
                        message += "字段名[SongListName]描述[]超长、字段最长[200]实际" + lists[i].SongListName.Length + "超过长度" + OutLength + ",";
                    }
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(message)) message = message.Substring(0, message.Length - 1);
        }

        ///<summary>
        ///生成插入Sql语句
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<returns>插入Sql语句字符串数组</returns>
        private ArrayList MarkInsertSql(List<SongList> lists)
        {
            ArrayList result = new ArrayList();
            foreach (SongList model in lists)
            {
                #region 拼写Sql语句
                string Sql = "insert into SongList(";
                Sql += "SongListId,";
                Sql += "SongListName,";
                Sql += "CreateTime,";
                Sql += "IsDefault";
                Sql += ") values(";
                Sql += "'" + FilteSQLStr(model.SongListId) + "',";
                Sql += "'" + FilteSQLStr(model.SongListName) + "',";
                Sql += "'" + model.CreateTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "',";
                Sql += "'" + model.IsDefault + "'";
                Sql += ")";
                #endregion
                result.Add(Sql);
            }
            return result;
        }

        ///<summary>
        ///生成更新Sql语句
        ///</summary>
        ///<param name="lists">数据List</param>
        ///<param name="SqlWhere">更新条件</param>
        ///<returns>更新Sql语句字符串数组</returns>
        private ArrayList MarkUpdateSql(List<SongList> lists, string SqlWhere)
        {
            ArrayList result = new ArrayList();
            foreach (SongList model in lists)
            {
                #region 拼写Sql语句
                string Sql = "update SongList set ";
                Sql += "SongListId='" + FilteSQLStr(model.SongListId) + "',";
                Sql += "SongListName='" + FilteSQLStr(model.SongListName) + "',";
                Sql += "CreateTime='" + model.CreateTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss") + "',";
                Sql += "IsDefault='" + model.IsDefault.GetValueOrDefault() + "'";
                if (!string.IsNullOrEmpty(SqlWhere))
                    Sql += " Where " + SqlWhere;
                #endregion
                result.Add(Sql);
            }
            return result;
        }
        #endregion

        #region 封装方法
        /// <summary>
        /// 过滤不安全的字符串
        /// </summary>
        /// <param name="Str">要过滤的值</param>
        /// <returns>返回结果</returns>
        private static string FilteSQLStr(object str)
        {
            if (str == null)
                return string.Empty;
            if (IsNumeric(str))
                return Convert.ToString(str);
            string Str = Convert.ToString(str);
            if (!string.IsNullOrEmpty(Str))
            {
                Str = Str.Replace("'", "");
                Str = Str.Replace("\"", "");
                Str = Str.Replace("&", "&amp");
                Str = Str.Replace("<", "&lt");
                Str = Str.Replace(">", "&gt");

                Str = Str.Replace("delete", "");
                Str = Str.Replace("update", "");
                Str = Str.Replace("insert", "");
            }
            return Str;
        }

        /// <summary>
        /// 判断object是否数字
        /// </summary>
        /// <param name="AObject">要判断的Object</param>
        /// <returns>是否数字</returns>       
        public static bool IsNumeric(object AObject)
        {
            return AObject is sbyte || AObject is byte ||
                AObject is short || AObject is ushort ||
                AObject is int || AObject is uint ||
                AObject is long || AObject is ulong ||
                AObject is double || AObject is char ||
                AObject is decimal || AObject is float ||
                AObject is double;
        }
        #endregion
    }
}
