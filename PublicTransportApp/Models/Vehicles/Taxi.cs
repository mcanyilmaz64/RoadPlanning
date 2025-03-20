namespace PublicTransportApp.Models.Vehicles
{
    public class Taxi:Vehicle
    {
		public override int OpeningFee()
		{
			return 10;
		}
		public override double CostPerKme(double k)
		{
			return k * 4;
		}
	}
}
