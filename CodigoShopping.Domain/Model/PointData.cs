using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodigoShopping.Domain.Model
{
    public class PointData
    {
        public int Id { get; set; }

        public int AppUserId { get; set; } 

        public AppUser AppUser { get; set; }

        public int NumberofPoint { get; set; }

        public decimal NumberPointofAmount { get; set; }


    }
}
