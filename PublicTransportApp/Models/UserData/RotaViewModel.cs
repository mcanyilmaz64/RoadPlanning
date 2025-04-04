using PublicTransportApp.Models.Graph;
using PublicTransportApp.Models.Passengers;
using PublicTransportApp.Models.Stops;
using System;

namespace PublicTransportApp.Models.UserData
{
	public class RouteViewModel
	{
		// User input
		public double StartLatitude { get; set; }
		public double StartLongitude { get; set; }
		public double DestinationLatitude { get; set; }
		public double DestinationLongitude { get; set; }
        public string AccessType { get; set; }
        public string? PassengerType { get; set; } 
		public Passenger? Passenger { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EstimatedArrivalTime { get; set; }
		//public string PaymentMethod { get; set; } // "Cash", "Card", "TransitCard"

        public List<Node> RouteNodes { get; set; } = new List<Node>();
        // Result fields
        public List<Stop> Route { get; set; } = new List<Stop>();
		public double TotalFare { get; set; }
		public double TotalDuration { get; set; }
		public double TotalDistance { get; set; }
        // NEW: destination access info
        public string DestinationAccessType { get; set; }
        public double DestinationAccessFare { get; set; }
        public double DestinationAccessDuration { get; set; }
    }
}
