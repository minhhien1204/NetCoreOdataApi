using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreOdataApi.Domain
{
    public class ParticipantViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool Delete { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public int TimeSpent { get; set; }
    }
}
