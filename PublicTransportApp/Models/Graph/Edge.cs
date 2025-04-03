using PublicTransportApp.Models.Stops;

namespace PublicTransportApp.Models.Graph
{
    public class Edge
    {
        public Stop From { get; set; }
        public Stop To { get; set; }
        public double Distance { get; set; } // km cinsinden
        public double Cost { get; set; }     // TL cinsinden
        public double Duration { get; set; } // dakika

        public Edge(Stop from, Stop to, double distance, double cost, double duration)
        {
            From = from;
            To = to;
            Distance = distance;
            Cost = cost;
            Duration = duration;
        }
    }
}
