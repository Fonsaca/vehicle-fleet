using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VF.Domain.DataTypes;

namespace VF.Domain.Models
{
    public abstract class Vehicle
    {

        public ChassisId ChassisId { get; set; }

        [Required()]
        public string Color { get; set; }

        public abstract string Type { get; }

        public abstract ushort NumberOfPassengers { get; }



    }
}
