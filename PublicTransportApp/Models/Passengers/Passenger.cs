namespace PublicTransportApp.Models.Passengers
{
    public abstract class Passenger
    {
        public abstract double TransPrice(double price);
        public double StartLocation { get; set; }
        public double TargetLocation { get; set; }

    }
}
