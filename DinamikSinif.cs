public class DinamikSinif : DynamicObject
    {
        public List<dynamic> GetData(DataTable dt)
        {
            List<dynamic> bendinamigim = new List<dynamic>();

            foreach (var item in dt.AsEnumerable())
            {                
                IDictionary<string, object> dn = new ExpandoObject();

                foreach (var column in dt.Columns.Cast<DataColumn>()) dn[column.ColumnName] = item[column];

                bendinamigim.Add(dn);
            }

            return bendinamigim;
        }
    }
