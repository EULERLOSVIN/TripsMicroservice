using System;
using System.Collections.Generic;

namespace TripsMicroservice.Entities;

public partial class TripFare
{
    public int IdTripFare { get; set; }

    public int IdTrip { get; set; }

    public decimal BaseFare { get; set; }

    public decimal DistanceFare { get; set; }

    public decimal TimeFare { get; set; }

    public decimal TotalFare { get; set; }

    public DateTime? CalculatedDate { get; set; }

    public virtual Trip IdTripNavigation { get; set; } = null!;
}
