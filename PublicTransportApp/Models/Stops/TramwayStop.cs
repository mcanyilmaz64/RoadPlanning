
namespace PublicTransportApp.Models.Stops
{
    public class TramwayStop : Stop
    {
        public TramwayStop(string ıd, string name, string type, double lat, double lon, bool sonDurak, List<NextStops> nextStops, Transfer transfer)
        {
            Id = ıd;
            Name = name;
            Type = type;
            Lat = lat;
            Lon = lon;
            SonDurak = sonDurak;
            NextStops = nextStops;
            Transfer = transfer;
        }
    }
}
