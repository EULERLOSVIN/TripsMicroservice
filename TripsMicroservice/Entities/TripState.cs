using System;
using System.Collections.Generic;

namespace TripsMicroservice.Entities;

public partial class TripState
{
    public int IdTripStates { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
