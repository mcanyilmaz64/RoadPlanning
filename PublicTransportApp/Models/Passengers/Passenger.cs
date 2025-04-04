namespace PublicTransportApp.Models.Passengers
{
    public abstract class Passenger
    {
        public virtual double ApplyDiscount(double cost)
        {
            return cost;
        }

    }
}
