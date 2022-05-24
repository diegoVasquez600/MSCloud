using System;
using System.Collections.Generic;

namespace MSCloudView.Models
{
    public partial class Athlete
    {
        public Athlete()
        {
            Results = new HashSet<Result>();
        }

        public int IdAthlete { get; set; }
        public string AthleteName { get; set; } = null!;
        public int IdCountry { get; set; }

        public virtual Country IdCountryNavigation { get; set; } = null!;
        public virtual ICollection<Result> Results { get; set; }
    }
}
