using System;
using System.Collections.Generic;
using PublicTransportApp.Models.Stops;

namespace PublicTransportApp.Models.Graph
{
    public enum EdgeWeightType
    {
        Time,
        Cost
    }

    public class Graph
    {
        private Dictionary<Stop, List<Edge>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<Stop, List<Edge>>();
        }

        public void AddEdge(Stop from, Stop to, double timeCost, double moneyCost)
        {
            if (!adjacencyList.ContainsKey(from))
                adjacencyList[from] = new List<Edge>();
            
            if (!adjacencyList.ContainsKey(to))
                adjacencyList[to] = new List<Edge>();

            adjacencyList[from].Add(new Edge(from, to, 0, moneyCost, timeCost));
            adjacencyList[to].Add(new Edge(to, from, 0, moneyCost, timeCost)); // İki yönlü graf
        }

        public List<Edge> GetEdges(Stop stop)
        {
            return adjacencyList.ContainsKey(stop) ? adjacencyList[stop] : new List<Edge>();
        }

        public List<Stop> FindShortestPath(Stop start, Stop end, EdgeWeightType weightType)
        {
            var distances = new Dictionary<Stop, double>();
            var previous = new Dictionary<Stop, Stop>();
            var unvisited = new HashSet<Stop>();

            // Başlangıç değerlerini ayarla
            foreach (var stop in adjacencyList.Keys)
            {
                distances[stop] = double.MaxValue;
                unvisited.Add(stop);
            }
            distances[start] = 0;

            while (unvisited.Count > 0)
            {
                // En küçük mesafeli durağı bul
                Stop current = null;
                double minDistance = double.MaxValue;
                foreach (var stop in unvisited)
                {
                    if (distances[stop] < minDistance)
                    {
                        minDistance = distances[stop];
                        current = stop;
                    }
                }

                if (current == null || current == end)
                    break;

                unvisited.Remove(current);

                // Komşu durakları kontrol et
                foreach (var edge in adjacencyList[current])
                {
                    double weight = weightType == EdgeWeightType.Time ? edge.Duration : edge.Cost;
                    double newDistance = distances[current] + weight;

                    if (newDistance < distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        previous[edge.To] = current;
                    }
                }
            }

            // Yolu oluştur
            var path = new List<Stop>();
            var currentStop = end;
            while (currentStop != null)
            {
                path.Add(currentStop);
                currentStop = previous.ContainsKey(currentStop) ? previous[currentStop] : null;
            }
            path.Reverse();

            return path;
        }
    }
}
