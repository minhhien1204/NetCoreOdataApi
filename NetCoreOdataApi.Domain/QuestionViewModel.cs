using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreOdataApi.Domain
{
    public class QuestionViewModel
    {
        public Guid Id { get; set; }
        //public DateTime CreateDate { get; set; }
        //public DateTime LastModifiedDate { get; set; }
        public bool Delete { get; set; }
        public string Content { get; set; }
        public string ImageQuestion { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public int Answer { get; set; }
        public string[] Options { get; set; }
    }
}
