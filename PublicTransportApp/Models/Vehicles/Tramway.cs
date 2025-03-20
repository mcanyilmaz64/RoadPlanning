using Microsoft.AspNetCore.Components.Sections;

namespace PublicTransportApp.Models.Vehicles
{
	public class Tramway : Vehicle
	{
		public override double CostPerKme(double km)
		{
			throw new NotImplementedException();
		}

		public override int OpeningFee()
		{
			throw new NotImplementedException();
		}
	}
}
