using System;
using System.Collections.Generic;

namespace TripsMicroservice.Entities;

public partial class Trip
{
    public int IdTrip { get; set; }

    public int IdPassengerAccount { get; set; }

    public int? IdDriverAccount { get; set; }

    public int IdTripStates { get; set; }

    public string OriginAddress { get; set; } = null!;

    public string DestinationAddress { get; set; } = null!;

    public decimal OriginLat { get; set; }

    public decimal OriginLng { get; set; }

    public decimal DestLat { get; set; }

    public decimal DestLng { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual TripState IdTripStatesNavigation { get; set; } = null!;

    public virtual ICollection<TripFare> TripFares { get; set; } = new List<TripFare>();
}
