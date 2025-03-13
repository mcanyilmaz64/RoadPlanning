using PublicTransportApp.Models.Vehicles;
using System.Collections.Generic;
using PublicTransportApp.Models.Stops;


namespace PublicTransportApp.Models
{
    public class PublicTransportData
    {
        public string City { get; set; }
        public Taxi Taxi { get; set; }

        public List<Stop> Stop { get; set; }

    }
}
