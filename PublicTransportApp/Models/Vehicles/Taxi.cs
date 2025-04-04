namespace PublicTransportApp.Models.Vehicles
{
    public class Taxi:Vehicle
    {
        public override string Name => "Taksi";

        public double OpeningFee => 10.0;
        public double PricePerKm => 4.0;

        public double CalculateFare(double distanceKm)
        {
            return OpeningFee + (distanceKm * PricePerKm);
        }

       
    }
}
