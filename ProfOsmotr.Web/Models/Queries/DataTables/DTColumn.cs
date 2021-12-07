﻿namespace ProfOsmotr.Web.Models.DataTables
{
    public class DTColumn
    {
        public string Data { get; set; }

        public string Name { get; set; }

        public bool Searchable { get; set; }

        public bool Orderable { get; set; }

        public DTSearch Search { get; set; }
    }
}