﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodigoShopping.Domain.Model
{
    public class PointSetting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PointMaxScore { get; set; }
        public decimal PointAmount { get; set; }


    }
}
