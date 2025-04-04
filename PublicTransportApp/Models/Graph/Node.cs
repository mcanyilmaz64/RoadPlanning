using PublicTransportApp.Models.Graph;
using PublicTransportApp.Models.Stops;
using PublicTransportApp.Models.Vehicles;

namespace PublicTransportApp.Models.Graph
{
	public class Node
	{
		public Stop Stop { get; set; }
		public List<Edge> Edges { get; set; }
        public Vehicle Vehicle { get; set; }

    }

}
