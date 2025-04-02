using PublicTransportApp.Models.Stops;

namespace PublicTransportApp.Services
{
    public static class LocationService
    {
        private const double EarthRadiusKm = 6371.0;

        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadiusKm * c;
        }

        private static double ToRadians(double degree) => degree * Math.PI / 180;

        public static Stop FindNearestStop(List<Stop> allStops, double userLat, double userLon)
        {
            Stop nearest = null;
            double minDistance = double.MaxValue;

            foreach (var stop in allStops)
            {
                double distance = CalculateDistance(userLat, userLon, stop.Lat, stop.Lon);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = stop;
                }
            }

            return nearest;
        }
    }
}
