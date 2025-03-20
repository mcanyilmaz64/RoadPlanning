using Newtonsoft.Json;

namespace PublicTransportApp.Models.Stops
{
	public class TransferStops
	{
		[JsonProperty("transferStopId")]
		public string TransferStopId { get; set; }

		[JsonProperty("transferSure")]
		public int TransferSure { get; set; }

		[JsonProperty("transferUcret")]
		public double TransferUcret { get; set; }
	}
}
