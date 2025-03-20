using System.Collections.Generic;

namespace PublicTransportApp.Models
{
    public class PathResult
    {
        public List<int> Path { get; set; } = new List<int>();
        public int TotalDistance { get; set; }
        public int ExploredNodes { get; set; }
    }
}
