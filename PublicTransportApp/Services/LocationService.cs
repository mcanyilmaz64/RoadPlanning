using PublicTransportApp.Models.Stops;
using PublicTransportApp.Models.Graph;


namespace PublicTransportApp.Services
{
	public class LocationService
	{
		public Node FindClosestNode(double lat, double lon, Dictionary<string, Node> graph)
		{
			Node closest = null;
			double minDistance = double.MaxValue;

			foreach (var node in graph.Values)
			{
				var distance = CalculateDistance(lat, lon, node.Stop.Lat, node.Stop.Lon);
				if (distance < minDistance)
				{
					minDistance = distance;
					closest = node;
				}
			}

			return closest;
		}

		public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
		{
			double R = 6371; // km cinsinden dünya yarıçapı
			double dLat = ToRadians(lat2 - lat1);
			double dLon = ToRadians(lon2 - lon1);
			double a =
				Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
				Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
				Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
			double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			return R * c;
		}

		private double ToRadians(double angle)
		{
			return angle * Math.PI / 180;
		}
	}

}
