using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Database.Context.DbEntities
{
    public class VehicleEntity
    {

        public string ChassisSerie { get; set; }

        public UInt32 ChassisNumber { get; set; }

        public string Type { get; set; }

        public VehicleTypeEntity VehicleType { get; set; }

        public string Color { get; set; }
    }
}
