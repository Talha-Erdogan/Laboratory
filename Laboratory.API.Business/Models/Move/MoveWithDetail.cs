using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.API.Business.Models.Move
{
    public class MoveWithDetail : Data.Entity.Move
    {
        //appliance detail column
        public string  Appliance_Name { get; set; }

        //lab detail column
        public string Lab_Name { get; set; }
    }
}
