using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PublicTransportApp.Models.Stops
{
    public class Stop
    {

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("lat")]
		public double Lat { get; set; }

		[JsonProperty("lon")]
		public double Lon { get; set; }

		[JsonProperty("sonDurak")]
		public bool SonDurak { get; set; }

		[JsonProperty("nextStops")]
		public List<NextStops> NextStops { get; set; }

		[JsonProperty("transfer")]
		public TransferStops Transfer { get; set; }

	}
}
