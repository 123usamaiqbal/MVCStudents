using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
namespace MVCStudents.Models

{
    public class DataTablesParameters
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public List<ColumnOrder> Order { get; set; }
        public Search Search { get; set; }
        public List<MyColumn> Columns { get; set; }
    }

    public class MyColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public Search Search { get; set; }
    }

    public class ColumnOrder
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public class Search
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }
}
