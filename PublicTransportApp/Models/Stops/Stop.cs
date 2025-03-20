using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace PublicTransportApp.Models.Stops
{
    public class Stop
    {
        public int Id { get; set; }

        [JsonPropertyName("Id")]
        public string IdStr { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool SonDurak { get; set; }
       
        //[JsonPropertyName("nextStops")]//if I dont identify this row it doesn't running correctly  
        public List<NextStops> NextStops { get; set; } = new List<NextStops>();
       
       // public NextStop NextStop { get; set; }
        public Transfer Transfer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

    }
}
