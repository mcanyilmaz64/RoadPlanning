namespace PublicTransportApp.Models.Graph
{
    public class Graph
    {
        public Dictionary<string, Node> Nodes = new();
        public Dictionary<string, List<Edge>> AdjacencyList = new();

        public void AddNode(Node node)
        {
            Nodes[node.Id] = node;
            AdjacencyList[node.Id] = new List<Edge>();
        }

        public void AddEdge(Node from, Node to, double distance, double cost, double duration)
        {
            Edge edge = new(from, to, distance, cost, duration);
            AdjacencyList[from.Id].Add(edge);
        }

        public List<Edge> GetEdges(string nodeId)
        {
            return AdjacencyList.ContainsKey(nodeId) ? AdjacencyList[nodeId] : new List<Edge>();
        }
    }

}
