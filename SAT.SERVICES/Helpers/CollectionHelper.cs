using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace SAT.SERVICES.Helpers {
    public class CollectionHelper
    {
        private CollectionHelper()
        {}

        public static DataTable Converter<T>(IList<T> list)
        {
            DataTable table = Criar<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }
                table.Rows.Add(row);
            }
            return table;
        }
        public static DataTable Criar<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            return table;
        }
    }
}