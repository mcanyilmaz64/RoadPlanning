namespace PublicTransportApp.Models.Vehicles
{
    public abstract class Vehicle
    {
        public abstract int OpeningFee();

        public abstract double CostPerKme(double km);
        
	}
}
