using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreOdataApi.Models
{
    public class Students
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }

    }
}
