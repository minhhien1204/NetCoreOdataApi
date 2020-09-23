using System;
using System.Collections.Generic;

namespace NetCoreOdataApi.Domain
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool Delete { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
