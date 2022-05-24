using System;
using System.Collections.Generic;

namespace MSCloudView.Models
{
    public partial class Country
    {
        public Country()
        {
            Athletes = new HashSet<Athlete>();
        }

        public int IdCountry { get; set; }
        public string CountryCode { get; set; } = null!;
        public string CountryName { get; set; } = null!;

        public virtual ICollection<Athlete> Athletes { get; set; }
    }
}
