using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreOdataApi.Core.Models.Quiz
{
    public class Question:Entity
    {
        public string Content { get; set; }
        public string ImageQuestion { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public int Answer { get; set; }

    }
}
