
using PublicTransportApp.Models.Stops;
using PublicTransportApp.Models.Graph;

namespace PublicTransportApp.Services
{
    

    public static class GraphBuilder
    {
        public static Graph BuildGraph(List<Stop> stops)
        {
            Graph graph = new();

            // Tüm durakları düğüm olarak ekle
            foreach (var stop in stops)
            {
                var node = new Node(stop.Id, stop.Lat, stop.Lon);
                graph.AddNode(node);
            }

            // Tüm durakların bağlantılarını kenar olarak ekle
            foreach (var stop in stops)
            {
                foreach (var next in stop.NextStops)
                {
                    if (graph.Nodes.TryGetValue(stop.Id, out var fromNode) &&
                        graph.Nodes.TryGetValue(next.StopId, out var toNode))
                    {
                        graph.AddEdge(fromNode, toNode, next.Mesafe, next.Ucret ?? 0, next.Sure ?? 0);
                    }
                }

                // Aktarma noktaları
                if (stop.Transfer != null)
                {
                    var transfer = stop.Transfer;

                    if (graph.Nodes.TryGetValue(stop.Id, out var fromNode) &&
                        graph.Nodes.TryGetValue(transfer.TransferStopId, out var toNode))
                    {
                        graph.AddEdge(fromNode, toNode, 0.2, transfer.TransferUcret, transfer.TransferSure);
                    }
                }
            }

            return graph;
        }
    }

}
