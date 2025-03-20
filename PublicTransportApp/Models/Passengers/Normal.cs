namespace PublicTransportApp.Models.Passengers
{
	public class Normal : Passenger//use colon=":" sign instead "extends" keyword
	{
		public override double TransPrice(double Price)
		{
			return Price;
		}
	}
}
