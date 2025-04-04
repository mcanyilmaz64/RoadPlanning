namespace PublicTransportApp.Models.Passengers
{
	public class Student : Passenger
	{
        public override double ApplyDiscount(double cost) => cost * 0.5;
    }
}
