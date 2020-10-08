using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreOdataApi.Core.Models.Quiz
{
    public class Account:Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
