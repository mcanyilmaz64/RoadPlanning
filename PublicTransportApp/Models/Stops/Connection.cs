namespace PublicTransportApp.Models
{
    public class Connection
    {
        public int FromNodeId { get; set; }
        public int ToNodeId { get; set; }
        public int Distance { get; set; }
    }
}
