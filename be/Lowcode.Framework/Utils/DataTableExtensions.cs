using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Lowcode.Framework.Utils
{

    /// <summary>
    /// extention of data table
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        ///  convert datatable to list object with columns mapping with properties of object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            var objFieldNames = (from PropertyInfo aProp in typeof(T).GetProperties(flags)
                                 select new
                                 {
                                     Name = aProp.Name,
                                     Type = Nullable.GetUnderlyingType(aProp.PropertyType) ??
                             aProp.PropertyType
                                 }).ToList();
            var dataTblFieldNames = (from DataColumn aHeader in dataTable.Columns
                                     select new
                                     {
                                         Name = aHeader.ColumnName,
                                         Type = aHeader.DataType
                                     }).ToList();
            var commonFields = objFieldNames.Intersect(dataTblFieldNames).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var aTSource = new T();
                foreach (var aField in commonFields)
                {
                    PropertyInfo propertyInfos = aTSource.GetType().GetProperty(aField.Name);
                    var value = (dataRow[aField.Name] == DBNull.Value) ?
                    null : dataRow[aField.Name]; //if database field is nullable
                    propertyInfos.SetValue(aTSource, value, null);
                }
                dataList.Add(aTSource);
            }
            return dataList;
        } 

        /// <summary>
        /// convert datatable to list object with columns mapping with properties of object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        //public static IList<T> ToList<T>(this DataTable table) where T : new()
        //{
        //    IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
        //    IList<T> result = new List<T>();

        //    foreach (var row in table.Rows)
        //    {
        //        var item = CreateItemFromRow<T>((DataRow)row, properties);
        //        result.Add(item);
        //    }

        //    return result;
        //}

        /// <summary>
        /// convert datarow to item with columns mapping with properties of object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        //private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        //{
        //    T item = new T();
        //    foreach (var property in properties)
        //    {
               
        //        try
        //        {
        //            property.SetValue(item, row[property.Name], null);
        //        }
        //        catch (Exception ex)
        //        {

        //            Logger.Instance.Error(ex.Message);
        //        }
                
        //    }
        //    return item;
        //}
    }

}