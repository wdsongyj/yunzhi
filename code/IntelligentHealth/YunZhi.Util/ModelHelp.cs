using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace YunZhi.Util
{
    public class ModelHandler<T> where T : new()
    {
        public List<T> TableToList(DataTable table)
        {
            if (null == table)
                return null;
            List<T> modelList = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                modelList.Add(DataRowToModel(row));
            }
            return modelList;
        }

        private T DataRowToModel(DataRow row)
        {
            T model = new T();
            //循环得到列
            foreach (DataColumn col in row.Table.Columns)
            {
                PropertyInfo info = model.GetType().GetProperty(col.ColumnName);
                if (info != null && row[col.ColumnName] != DBNull.Value)
                {
                    object val = row[col.ColumnName];
                    if (info.PropertyType.Name == "String")
                    {
                        val = val.ToString().TrimEnd();
                    }
                    Type infoType = info.PropertyType;
                    object obj = ChanageTypeExtension.ChanageType(val, infoType);
                    info.SetValue(model, obj, null);
                }
            }
            return model;
        }


        public DataTable ListToTable(List<T> modelList)
        {
            if (modelList == null || modelList.Count == 0)
                return null;
            //创建表
            DataTable tableResult = CreateTable(modelList[0]);
            foreach (T a in modelList)
            {
                DataRow row = tableResult.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    if (propertyInfo.GetValue(a, null) != null)
                        row[propertyInfo.Name] = propertyInfo.GetValue(a, null);
                }
                tableResult.Rows.Add(row);
            }           
            return tableResult;
        }

        public DataTable ListToTablebyState(List<T> modelList)
        {
            if (modelList == null || modelList.Count == 0)
                return null;
            //创建表
            DataTable tableResult = CreateTable(modelList[0]);
            List<DataRow> addRows = new List<DataRow>();
            //首先先添加修改和删除的
            foreach (T a in modelList)
            {
                PropertyInfo statePropertyInfo = typeof(T).GetProperty("ModelInfoState");
                string stateValue = string.Empty;
                if (statePropertyInfo != null)
                {
                    stateValue = statePropertyInfo.GetValue(a, null).ToString();
                }
                DataRow row = tableResult.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    if (propertyInfo.GetValue(a, null) != null)
                        row[propertyInfo.Name] = propertyInfo.GetValue(a, null);
                }
                if (stateValue == "Added")
                {
                    addRows.Add(row);
                }
                else
                {
                    tableResult.Rows.Add(row);
                }
            }
            tableResult.AcceptChanges();
            DataRow[] deleteRows = tableResult.Select("ModelInfoState='Deleted'");
            if (deleteRows != null && deleteRows.Length > 0)
            {
                foreach (DataRow deleteRow in deleteRows)
                {
                    deleteRow.Delete();
                }
            }
            DataRow[] updateRows = tableResult.Select("ModelInfoState='Updated'");
            if (updateRows != null && updateRows.Length > 0)
            {
                foreach (DataRow updateRow in updateRows)
                {
                    updateRow["ModelInfoState"] = updateRow["ModelInfoState"];
                }
            }
            foreach (DataRow row in addRows)
            {
                tableResult.Rows.Add(row);
            }
            return tableResult;
        }


        //public DataTable ListToTable(List<T> modelList)
        //{
        //    if (modelList == null || modelList.Count == 0)
        //        return null;
        //    //创建表
        //    DataTable tableResult = CreateTable(modelList[0]);
        //    foreach (T a in modelList)
        //    {
        //        DataRow row = tableResult.NewRow();
        //        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
        //        {
        //            if (propertyInfo.GetValue(a, null) != null)
        //                row[propertyInfo.Name] = propertyInfo.GetValue(a, null);
        //        }
        //        tableResult.Rows.Add(row);
        //    }
        //    return tableResult;
        //}

        /// <summary>
        /// 创建dataTable
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private DataTable CreateTable(T model)
        {
            string tableName = typeof(T).Name;
            DataTable dataTable = new DataTable(tableName);
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                Type ty = propertyInfo.PropertyType;
                if (ty.IsGenericType && ty.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(propertyInfo.PropertyType);
                    ty = nullableConverter.UnderlyingType;
                }
                dataTable.Columns.Add(new DataColumn(propertyInfo.Name, ty));
            }
            return dataTable;
        }
    }

    public static class ChanageTypeExtension
    {
        /// <summary>
        /// 可为空类型扩展类，转换为基础类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="convertsionType"></param>
        /// <returns></returns>
        public static object ChanageType(this object value, Type convertsionType)
        {
            if (convertsionType.IsGenericType &&
                convertsionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value.ToString().Length == 0)
                {
                    return null;
                }
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(convertsionType);
                convertsionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, convertsionType);
        }
    }

}
