using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Models.Move
{
    public class MoveWithDetail : Move
    {
        //appliance detail column
        public string Appliance_Name { get; set; }

        //lab detail column
        public string Lab_Name { get; set; }
    }
}
