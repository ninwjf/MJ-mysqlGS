using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardManage.Model
{
    public class BuildingCode : ICloneable
    {
        public string AreaCode { get; set; }
        public string BuildCode { get; set; }
        public string UnitCode { get; set; }
        public string RoomCode { get; set; }

        public BuildingCode()
        {
            AreaCode = "";
            BuildCode = "";
            UnitCode = "";
            RoomCode = "";
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
