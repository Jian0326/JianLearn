using System;
using Mono.Data.Sqlite;
using UnityEngine;

public class DbAccess
{
    private SqliteConnection dbConnection;
    private SqliteCommand sqliteCommand;
    private SqliteDataReader sdReader;
    public DbAccess(string connectionString)
    {
        OpenDB(connectionString);
    }

    public DbAccess()
    {

    }
    //链接数据库
    public void OpenDB(string connectionString)
    {
        try
        {
            dbConnection = new SqliteConnection(connectionString);
            dbConnection.Open();
        }
        catch (Exception e)
        {
            string temp1 = e.ToString();
            Debug.Log(temp1);
        }
    }
    //关闭数据库
    public void CloseDB()
    {
        if (null != sqliteCommand)
        {
            sqliteCommand.Dispose();
        }
        if (null != sdReader)
        {
            sdReader.Close();
            sdReader.Dispose();
        }
        if (null != dbConnection)
        {
            dbConnection.Close();
            dbConnection.Dispose();
        }
        dbConnection = null;
        sqliteCommand = null;
        sdReader = null;
    }
    //查询数据库
    public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        if (null == sqliteCommand)
        {
            sqliteCommand = dbConnection.CreateCommand();
        }
        sqliteCommand.CommandText = sqlQuery;
        sdReader = sqliteCommand.ExecuteReader();
        return sdReader;
    }
    //读取table
    public SqliteDataReader ReadFullTable(string tableName)
    {
        string query = "SELECT * FROM " + tableName;
        return ExecuteQuery(query);
    }
    //插入
    public SqliteDataReader InsertInto(string tableName, string[] values)
    {
        string query = "INSERT INTO " + tableName + " VALUES ('" + values[0] + "'";
        for (int i = 1; i < values.Length; ++i)
        {
            query += ",'" + values[i] + "'";
        }
        query += ")";
        return ExecuteQuery(query);
    }
    //更新
    public SqliteDataReader UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {
        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];
        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += ", " + cols[i] + " =" + colsvalues[i];
        }
        query += " WHERE " + selectkey + " = " + selectvalue + " ";
        return ExecuteQuery(query);
    }
    //删除
    public SqliteDataReader Delete(string tableName, string[] cols, string[] colsvalues)
    {
        string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];
        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += " or " + cols[i] + " = " + colsvalues[i];
        }
        Debug.Log(query);
        return ExecuteQuery(query);
    }
    //插入指定数据到表
    public SqliteDataReader InsertIntoSpecific(string tableName, string[] cols, string[] values)
    {
        if (cols.Length != values.Length)
        {
            throw new SqliteException("columns.Length != values.Length");
        }

        string query = "INSERT INTO " + tableName + "(" + cols[0];
        for (int i = 1; i < cols.Length; ++i)
        {
            query += ", " + cols[i];
        }
        query += ") VALUES (" + values[0];

        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }
    //动态删除表
    public SqliteDataReader DeleteContents(string tableName)
    {
        string query = "DELETE FROM " + tableName;
        return ExecuteQuery(query);
    }
    public bool IsTable(string name)
    {
        if (name == string.Empty)
        {
            Debug.LogFormat("sql table name = Empty {0}", name);
            return false;
        }
        if (null == sqliteCommand)
        {
            sqliteCommand = dbConnection.CreateCommand();
        }
        sqliteCommand.CommandText = string.Format("SELECT COUNT(*) FROM sqlite_master where type='table' and name='{0}';", name);
        if (1 == Convert.ToInt32(sqliteCommand.ExecuteScalar()))
        {
            return true;
        }
        return false;
    }

    // 创建表
    public SqliteDataReader CreateTable(string name, string[] col, string[] colType)
    {
        if (IsTable(name))
        {
            return ReadFullTable(name);
        }

        if (col.Length != colType.Length)
        {
            throw new SqliteException("columns.Length != colType.Length");
        }

        string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];

        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i] + " " + colType[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }
    //根据查询条件 动态查询数据信息
    // tablename table得名字
    // items 查找的相
    // col 要比较的
    // operation 比较的符号
    // values 要比较的参数
    // db.SelectWhere(tablename, new string[] { "name" }, new string[] { "name" }, new string[] { "=" }, new string[] { nameText.text });
    public SqliteDataReader SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
    {
        if (col.Length != operation.Length || operation.Length != values.Length)
        {
            throw new SqliteException("col.Length != operation.Length != values.Length");
        }

        string query = "SELECT " + items[0];

        for (int i = 1; i < items.Length; ++i)
        {
            query += ", " + items[i];
        }

        query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";
        for (int i = 1; i < col.Length; ++i)
        {
            query += " AND " + col[i] + operation[i] + "'" + values[0] + "' ";
        }
        return ExecuteQuery(query);
    }
}


