using Newtonsoft.Json;

namespace PublicTransportApp.Models.Stops
{
	public class NextStops
	{
		[JsonProperty("stopId")]
		public string StopId { get; set; }

		[JsonProperty("mesafe")]
		public double Mesafe { get; set; }

		[JsonProperty("sure")]
		public int? Sure { get; set; }

		[JsonProperty("ucret")]
		public double? Ucret { get; set; }
	}
}
