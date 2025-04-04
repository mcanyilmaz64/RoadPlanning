using PublicTransportApp.Models.Stops;
using PublicTransportApp.Models.Vehicles;

namespace PublicTransportApp.Models.Graph

{
    public class GraphBuilder
    {
        public Dictionary<string, Node> BuildGraph(List<Stop> stops)
        {
            var graph = new Dictionary<string, Node>();


            // Node'ları oluştur
            foreach (var stop in stops)
            {
                Vehicle vehicle = stop.Type switch
                {
                    "bus" => new Bus(),
                    "tram" => new Tramway(),
                    _ => null
                };
                graph[stop.Id] = new Node
                {
                    Stop = stop,
                    Edges = new List<Edge>()
                };
            }

            // NextStops ekle
            foreach (var stop in stops)
            {
                var fromNode = graph[stop.Id];


                foreach (var next in stop.NextStops)
                {
                    if (graph.TryGetValue(next.StopId, out var toNode))
                    {
                        fromNode.Edges.Add(new Edge
                        {
                            From = fromNode,
                            To = toNode,
                            Duration = next.Sure ?? 0,
                            Cost = next.Ucret ?? 0,
                            Distance = next.Mesafe,
                            Vehicle = fromNode.Vehicle
                        });
                    }
                }


                // Transfer varsa
                if (stop.Transfer != null && graph.ContainsKey(stop.Transfer.TransferStopId))
                {
                    var transferNode = graph[stop.Transfer.TransferStopId];

                    fromNode.Edges.Add(new Edge
                    {
                        From = fromNode,
                        To = transferNode,
                        Duration = stop.Transfer.TransferSure,
                        Cost = stop.Transfer.TransferUcret,
                        Distance = 0.1

                    });
                }
            }

            return graph;
        }
    }

}