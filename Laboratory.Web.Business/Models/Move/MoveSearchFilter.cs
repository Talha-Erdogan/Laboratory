using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Models.Move
{
    public class MoveSearchFilter
    {

        // paging
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        // sorting
        public string SortOn { get; set; }
        public string SortDirection { get; set; }

        // filters        
        public string Filter_Appliance_Name { get; set; }
        public string Filter_Lab_Name { get; set; }
    }
}
