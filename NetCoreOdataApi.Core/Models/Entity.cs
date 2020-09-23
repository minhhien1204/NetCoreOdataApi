using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreOdataApi.Core.Models
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool Delete { get; set; }
        public Entity()
        {
            Id = new Guid();
        }
       
    }
}
