using System;
using System.Collections.Generic;

namespace MSCloudView.Models
{
    public partial class Result
    {
        public int IdResult { get; set; }
        public int IdAthlete { get; set; }
        public int ArranqueKg { get; set; }
        public int EnvionKg { get; set; }
        public int TotalPesoKg { get; set; }

        public virtual Athlete IdAthleteNavigation { get; set; } = null!;
    }
}
