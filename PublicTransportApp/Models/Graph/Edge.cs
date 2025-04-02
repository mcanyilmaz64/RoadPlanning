namespace PublicTransportApp.Models.Graph
{
    public class Edge
    {
        public Node From { get; set; }
        public Node To { get; set; }
        public double Distance { get; set; } // km cinsinden
        public double Cost { get; set; }     // TL cinsinden
        public double Duration { get; set; } // dakika

        public Edge(Node from, Node to, double distance, double cost, double duration)
        {
            From = from;
            To = to;
            Distance = distance;
            Cost = cost;
            Duration = duration;
        }
    }

}
