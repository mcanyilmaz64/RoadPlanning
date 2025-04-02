namespace PublicTransportApp.Models.Graph
{
    public class Node
    {
        public string Id { get; set; } // Durak adı/id'si
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Node(string id, double latitude, double longitude)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
        }
    }

}
