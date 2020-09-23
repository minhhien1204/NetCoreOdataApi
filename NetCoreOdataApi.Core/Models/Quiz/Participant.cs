using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreOdataApi.Core.Models.Quiz
{
    public class Participant: Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public int TimeSpent { get; set; }
        public Participant()
        {
            this.Score = 0;
        }
    }
}
