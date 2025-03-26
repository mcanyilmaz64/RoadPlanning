using System.Text.Json.Serialization;

namespace PublicTransportApp.Models.Stops
{
    public class Stop
    {
        


        [JsonPropertyName("id")]
        public string IdStr { get; set; }


        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lon")]
        public double Lon { get; set; }

        [JsonPropertyName("sonDurak")]
        public bool SonDurak { get; set; }

        [JsonPropertyName("nextStops")]

        //[JsonPropertyName("nextStops")]//if I dont identify this row it doesn't running correctly  
        public List<NextStops> NextStops { get; set; } = new List<NextStops>();

        // public NextStop NextStop { get; set; }
        [JsonPropertyName("transfer")]
        public Transfer Transfer { get; set; } = new Transfer();//sadece tek nesneydi önceden şimdi liste oldu 

        [JsonIgnore]

        public int X { get; set; }
        [JsonIgnore]

        public int Y { get; set; }

        [JsonIgnore]//if we  didn't write this ignore jsonserializer methods  trying to serialize these too.

        public int Id { get; set; }


        [JsonIgnore]
        // Creating a dictionary
        public Dictionary<string, float> TimeCost = new Dictionary<string, float>();

        [JsonIgnore]
        // Creating a dictionary
        public Dictionary<string, float> MoneyCost = new Dictionary<string, float>();

        [JsonIgnore]
        public int TheDistanceFromUserToStop;

        [JsonIgnore]
        public int TheDistanceFromStopToDestination;



       





    }
}
