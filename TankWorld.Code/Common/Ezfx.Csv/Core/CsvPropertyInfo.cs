using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Ezfx.Csv
{
    public class CsvPropertyInfo
    {
        public SystemCsvColumnAttribute Attribute { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }
}
