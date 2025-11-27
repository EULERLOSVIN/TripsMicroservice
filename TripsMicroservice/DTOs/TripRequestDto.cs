namespace TripsMicroservice.DTOs
{
    public class TripRequestDto
    {
        public int PassengerId { get; set; }
        public string OriginAddress { get; set; } = string.Empty;
        public string DestinationAddress { get; set; } = string.Empty;
        public decimal OriginLat { get; set; }
        public decimal OriginLng { get; set; }
        public decimal DestLat { get; set; }
        public decimal DestLng { get; set; }
    }
}