namespace PublicTransportApp.Models.Passengers
{
	public class Student : Passenger
	{
		public override double TransPrice(double price)
		{
			return price/2;
		}
	}
}
