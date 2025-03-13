namespace PublicTransportApp.Models.Stops
{
    public class Stop
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool SonDurak { get; set; }
        public List<NextStops> NextStops { get; set; }
        public Transfer Transfer { get; set; }

    }
}
