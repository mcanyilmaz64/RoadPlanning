using Newtonsoft.Json;

namespace PublicTransportApp.Models.Stops
{
	public class RootModel
	{
		[JsonProperty("duraklar")]
        public List<Stop> duraklar { get; set; }
		
	}
}
