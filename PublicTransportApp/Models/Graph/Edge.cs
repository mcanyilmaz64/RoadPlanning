
using PublicTransportApp.Models.Vehicles;

namespace PublicTransportApp.Models.Graph
{
public class Edge
{
	public Node From { get; set; }
	public Node To { get; set; }
	public double Distance { get; set; }
	public int Duration { get; set; }
	public double Cost { get; set; }
    public Vehicle Vehicle { get; set; }

    }

}
