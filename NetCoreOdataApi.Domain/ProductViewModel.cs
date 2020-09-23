using System;

namespace NetCoreOdataApi.Domain
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool Delete { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public Guid CateId { get; set; }
        public string CateName { get; set; }
    }
}
