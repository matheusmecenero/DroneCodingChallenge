namespace DroneChallenge.Domain
{
	public class Drone
	{
		public string Name { get; set; } = string.Empty;
		public int Weight { get; set; }
		public int LeftLoadWeight { get; set; }
		public List<Location> Trips { get; set; } = new List<Location>();
		public int TripsCount { get; set; } = 0;
	}
}