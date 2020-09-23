using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreOdataApi.Core.Models
{
    public class Product:Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public virtual Category Category { get; set; }
    }
}
