namespace TripsMicroservice.Events;

public class TripCreatedEvent
{
    public int TripId { get; set; }
    public int PassengerId { get; set; }
    public string OriginAddress { get; set; } = string.Empty;
    public string DestinationAddress { get; set; } = string.Empty;
    public decimal EstimatedFare { get; set; }
    public DateTime CreatedAt { get; set; }
}
