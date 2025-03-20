using PublicTransportApp.Models.Stops;
using System.Collections.Generic;

namespace PublicTransportApp.Models
{
    public class RouteViewModel
    {
        public List<Stop> Nodes { get; set; }// DÜZENLEME GEREKEBİLİR
        public Dictionary<string,int> Distances { get; set; }
        public int? StartNodeId { get; set; }
        public int? EndNodeId { get; set; }
        public PathResult PathResult { get; set; }
    }
}
