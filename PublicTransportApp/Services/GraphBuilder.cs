using PublicTransportApp.Models.Stops;
using PublicTransportApp.Models.Graph;

namespace PublicTransportApp.Services
{
    public static class GraphBuilder
    {
        public static Graph BuildGraph(List<Stop> stops)
        {
            Graph graph = new();

            // Tüm durakların bağlantılarını kenar olarak ekle
            foreach (var stop in stops)
            {
                foreach (var next in stop.NextStops)
                {
                    // Hedef durağı bul
                    var toStop = stops.FirstOrDefault(s => s.Id == next.StopId);
                    if (toStop != null)
                    {
                        // Zaman maliyeti (dakika) ve para maliyeti (TL) olarak ekle
                        graph.AddEdge(stop, toStop, next.Sure ?? 0, next.Ucret ?? 0);
                    }
                }

                // Aktarma noktaları
                if (stop.Transfer != null)
                {
                    var transfer = stop.Transfer;
                    
                    // Transfer durağını bul
                    var transferStop = stops.FirstOrDefault(s => s.Id == transfer.TransferStopId);
                    if (transferStop != null)
                    {
                        // Transfer için zaman maliyeti (dakika) ve para maliyeti (TL) olarak ekle
                        graph.AddEdge(stop, transferStop, transfer.TransferSure, transfer.TransferUcret);
                    }
                }
            }

            return graph;
        }
    }
}
