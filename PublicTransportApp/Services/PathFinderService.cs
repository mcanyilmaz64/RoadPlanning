using System;
using System.Collections.Generic;
using System.Linq;
using PublicTransportApp.Models;
using PublicTransportApp.Models.Stops;

namespace PublicTransportApp.Services
{
    public class PathFinderService
    {
        private class PathNode
        {
            public int Id { get; set; }
            public double G { get; set; }
            public double H { get; set; }
            public double F => G + H;
            public PathNode Parent { get; set; }
        }

        private readonly List<Stop> _nodes;
        private readonly Dictionary<string, int> _distances;

        public PathFinderService()
        {
            _nodes = InitializeNodes();
            _distances = InitializeDistances();
        }

        public List<Stop> GetNodes()
        {
            return _nodes;
        }

        public Dictionary<string, int> GetDistances()
        {
            return _distances;
        }

        public PathResult FindPath(int startId, int endId)
        {
            if (startId == endId)
            {
                return new PathResult
                {
                    Path = new List<int> { startId },
                    TotalDistance = 0,
                    ExploredNodes = 1
                };
            }

            var openList = new List<PathNode>();
            var closedList = new List<PathNode>();
            var startNode = new PathNode { Id = startId };
            
            startNode.H = CalculateHeuristic(startId, endId);
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                var currentNode = openList.OrderBy(n => n.F).First();
                
                if (currentNode.Id == endId)
                {
                    return ReconstructPath(currentNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (var neighbor in GetNeighbors(currentNode.Id))
                {
                    if (closedList.Any(n => n.Id == neighbor.Id))
                    {
                        continue;
                    }

                    var gScore = currentNode.G + neighbor.Distance;
                    var neighborNode = openList.FirstOrDefault(n => n.Id == neighbor.Id);

                    if (neighborNode == null)
                    {
                        neighborNode = new PathNode
                        {
                            Id = neighbor.Id,
                            G = gScore,
                            H = CalculateHeuristic(neighbor.Id, endId),
                            Parent = currentNode
                        };
                        openList.Add(neighborNode);
                    }
                    else if (gScore < neighborNode.G)
                    {
                        neighborNode.G = gScore;
                        neighborNode.Parent = currentNode;
                    }
                }
            }

            return new PathResult
            {
                Path = new List<int>(),
                TotalDistance = 0,
                ExploredNodes = closedList.Count + openList.Count
            };
        }

        private double CalculateHeuristic(int nodeId1, int nodeId2)
        {
            var node1 = _nodes.Find(n => n.Id == nodeId1);
            var node2 = _nodes.Find(n => n.Id == nodeId2);

            if (node1 != null && node2 != null)
            {
                var dx = node1.X - node2.X;
                var dy = node1.Y - node2.Y;
                return Math.Sqrt(dx * dx + dy * dy) / 10;
            }

            return 0;
        }

        private List<(int Id, int Distance)> GetNeighbors(int nodeId)
        {
            var neighbors = new List<(int Id, int Distance)>();

            foreach (var entry in _distances)
            {
                var parts = entry.Key.Split('-');
                var fromId = int.Parse(parts[0]);
                var toId = int.Parse(parts[1]);

                if (fromId == nodeId)
                {
                    neighbors.Add((toId, entry.Value));
                }
            }

            return neighbors;
        }

        private PathResult ReconstructPath(PathNode endNode)
        {
            var path = new List<int>();
            var current = endNode;
            var totalDistance = 0;
            int? lastId = null;

            while (current != null)
            {
                path.Insert(0, current.Id);

                if (lastId.HasValue)
                {
                    var key = $"{current.Id}-{lastId}";
                    if (_distances.TryGetValue(key, out var distance))
                    {
                        totalDistance += distance;
                    }
                }

                lastId = current.Id;
                current = current.Parent;
            }

            return new PathResult
            {
                Path = path,
                TotalDistance = totalDistance,
                ExploredNodes = 0
            };
        }

        private List<Stop> InitializeNodes()
        {
            return new List<Stop>
            {
                new Stop { Id = 0, Name = "İstanbul", Type = "A", X = 150, Y = 100 },
                new Stop { Id = 1, Name = "Ankara", Type = "A", X = 350, Y = 150 },
                new Stop { Id = 2, Name = "İzmir", Type = "A", X = 100, Y = 300 },
                new Stop { Id = 3, Name = "Antalya", Type = "A", X = 250, Y = 400 },
                new Stop { Id = 4, Name = "Konya", Type = "A", X = 400, Y = 300 },
                new Stop { Id = 5, Name = "Bursa", Type = "A", X = 200, Y = 200 },
                new Stop { Id = 6, Name = "Adana", Type = "B", X = 500, Y = 350 },
                new Stop { Id = 7, Name = "Trabzon", Type = "B", X = 600, Y = 100 },
                new Stop { Id = 8, Name = "Diyarbakır", Type = "B", X = 550, Y = 250 },
                new Stop { Id = 9, Name = "Samsun", Type = "B", X = 450, Y = 50 }
            };
        }

        private Dictionary<string, int> InitializeDistances()
        {
            return new Dictionary<string, int>
            {
                { "0-1", 450 }, { "0-5", 110 }, { "0-7", 1050 }, { "0-9", 750 },
                { "1-4", 260 }, { "1-5", 380 }, { "1-7", 720 }, { "1-8", 670 }, { "1-9", 415 },
                { "2-3", 350 }, { "2-5", 310 },
                { "3-2", 350 }, { "3-4", 320 }, { "3-6", 450 },
                { "4-1", 260 }, { "4-3", 320 }, { "4-6", 300 }, { "4-8", 800 },
                { "5-0", 110 }, { "5-1", 380 }, { "5-2", 310 },
                { "6-3", 450 }, { "6-4", 300 }, { "6-8", 550 },
                { "7-0", 1050 }, { "7-1", 720 }, { "7-9", 400 },
                { "8-1", 670 }, { "8-4", 800 }, { "8-6", 550 },
                { "9-0", 750 }, { "9-1", 415 }, { "9-7", 400 }
            };
        }
    }
}
