using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Frame;
using Mono.Data.Sqlite;
using System.Reflection;
using System;

public class HeroTableData
{
    public int Id { get; set; }
    public string HeroName { get; set; }
    public int HeroOccupation { get; set; }
}

/// <summary>
/// 本地数据库（静态表格）读取类
/// </summary>
public class Database : Singleton<Database> {

    delegate object FuncGetById(int id);
    delegate object[] FuncGetAll();
    Dictionary<Type, FuncGetById> dicGetById = new Dictionary<Type, FuncGetById>();
    Dictionary<Type, FuncGetAll> dicGetAll = new Dictionary<Type, FuncGetAll>();

    DbAccess db;

    /// <summary>
    /// 打开本地数据库
    /// </summary>
    public void Open()
    {
        db = new DbAccess();
        string streamingPath = Application.streamingAssetsPath + "/";
        string dbFilename = "C:/iann1978.NewFrame/trunk/Csv/StaticTables.db";
        //string dbFilename = "E:/StaticTables.db";
        Debug.Log("localdb:" + dbFilename);        
        db.OpenDB("data source=" + dbFilename);
        //SqliteDataReader sqReader = db.ExecuteQuery("Select * from hero");
        //while (sqReader.Read())
        //{
        //    Debug.Log(sqReader.GetString(sqReader.GetOrdinal("HeroName")));
        //}
    }

    /// <summary>
    /// used for lua to read
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Regist<T>() where T : class, new()
    {
        dicGetById[typeof(T)] = GetById<T>;
        dicGetAll[typeof(T)] = GetAll<T>;
    }

    /// <summary>
    /// 关闭本地数据库
    /// </summary>
    public void Close()
    {
        db.CloseSqlConnection();
    }

    T CovertFromReader<T>(SqliteDataReader sqReader) where T : class, new()
    {
        Type type = typeof(T);
        T tableData = new T();
        PropertyInfo[] props = type.GetProperties();
        foreach (PropertyInfo propInfo in props)
        {
            if (propInfo.PropertyType == typeof(int))
            {
                int value = sqReader.GetInt32(sqReader.GetOrdinal(propInfo.Name));
                propInfo.SetValue(tableData, value, null);
            }
            if (propInfo.PropertyType == typeof(string))
            {
                string value = sqReader.GetString(sqReader.GetOrdinal(propInfo.Name));
                propInfo.SetValue(tableData, value, null);
            }
            if (propInfo.PropertyType.IsEnum)
            {
                string strValue = sqReader.GetString(sqReader.GetOrdinal(propInfo.Name));
                object value = Enum.Parse(propInfo.PropertyType, strValue);
                propInfo.SetValue(tableData, value, null);
            }
        }
        return tableData;

    }

    /// <summary>
    /// 获取表格数据
    /// </summary>
    /// <typeparam name="T">表格数据的类型</typeparam>
    /// <param name="id">数据的Id</param>
    /// <returns>返回表格数据</returns>
    public T GetById<T>(int id) where T : class, new()
    {
        T tableData = null;
        Type type = typeof(T);
        string tableName = type.Name.Substring(0, type.Name.IndexOf("TableData"));

        //tableName = "hero";
        string cmd = String.Format("select * from {0} where id={1}", tableName, id);
        SqliteDataReader sqReader = db.ExecuteQuery(cmd);
        if (sqReader.Read())
        {
            tableData = CovertFromReader<T>(sqReader);
        }
        
        return tableData;
    }

    public object GetById(Type type, int id)
    {
        return dicGetById[type](id);
    }

    //public HeroTableData[] GetAllHeroTableData()
    //{
    //    return GetAll<HeroTableData>();
    //}

    //public HeroTableData GetHeroTableDataById(int id)
    //{
    //    return GetById<HeroTableData>(id);
    //}

    public T[] GetAll<T>() where T : class, new()
    {
        Type type = typeof(T);
        List<T> list = new List<T>();
        string tableName = type.Name.Substring(0, type.Name.IndexOf("TableData"));

        string cmd = String.Format("select * from {0}", tableName);
        SqliteDataReader sqReader = db.ExecuteQuery(cmd);
        while (sqReader.Read())
        {
            T tableData = CovertFromReader<T>(sqReader);
            list.Add(tableData);
        }

        return list.ToArray();

    }

    public object[] GetAll(Type type)
    {
        return dicGetAll[type]();
    }

}
