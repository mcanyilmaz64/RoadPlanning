using PublicTransportApp.Models.Stops;

namespace PublicTransportApp.Services
{
    public static class LocationService
    {
        public static Stop FindNearestStop(List<Stop> stops, double userLat, double userLon)
        {
            Stop nearestStop = null;
            double minDistance = double.MaxValue;

            foreach (var stop in stops)
            {
                double distance = CalculateDistance(userLat, userLon, stop.Lat, stop.Lon);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestStop = stop;
                }
            }

            return nearestStop;
        }

        private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Dünya'nın yarıçapı (km)

            var dLat = ToRad(lat2 - lat1);
            var dLon = ToRad(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = R * c;

            return distance;
        }

        private static double ToRad(double degree)
        {
            return degree * Math.PI / 180;
        }
    }
}
