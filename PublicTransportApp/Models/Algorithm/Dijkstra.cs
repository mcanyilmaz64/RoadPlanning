using PublicTransportApp.Models.Graph;

namespace PublicTransportApp.Models.Algorithm
{
	public class Dijkstra
	{
		public List<Node> FindShortestPath(Node startNode, Node endNode, Func<Edge, double> weightSelector)
		{
			var distances = new Dictionary<Node, double>();
			var previousNodes = new Dictionary<Node, Node>();
			var queue = new PriorityQueue<Node, double>();

			// 1. Başlangıç dışındaki tüm mesafeleri sonsuz yap
			foreach (var node in GetAllNodes(startNode))
			{
				distances[node] = double.PositiveInfinity;
			}

			distances[startNode] = 0;
			queue.Enqueue(startNode, 0);

			while (queue.Count > 0)
			{
				var current = queue.Dequeue();

				if (current == endNode)
					break;

				foreach (var edge in current.Edges)
				{
					var neighbor = edge.To;
					var weight = weightSelector(edge); // Süre, mesafe veya ücret seçimi
					var altDistance = distances[current] + weight;

					if (altDistance < distances[neighbor])
					{
						distances[neighbor] = altDistance;
						previousNodes[neighbor] = current;
						queue.Enqueue(neighbor, altDistance);
					}
				}
			}

			return ReconstructPath(previousNodes, startNode, endNode);
		}

		private List<Node> ReconstructPath(Dictionary<Node, Node> previousNodes, Node start, Node end)
		{
			var path = new List<Node>();
			var current = end;

			while (current != null && previousNodes.ContainsKey(current))
			{
				path.Insert(0, current);
				current = previousNodes[current];
			}

			if (current == start)
			{
				path.Insert(0, start);
			}

			return path;
		}

		private HashSet<Node> GetAllNodes(Node startNode)
		{
			// Basit bir BFS ile tüm node'ları topla
			var visited = new HashSet<Node>();
			var queue = new Queue<Node>();
			queue.Enqueue(startNode);

			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				if (!visited.Contains(current))
				{
					visited.Add(current);
					foreach (var edge in current.Edges)
					{
						queue.Enqueue(edge.To);
					}
				}
			}

			return visited;
		}
	}

}
