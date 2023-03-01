namespace DroneChallenge.Domain
{
	public class ResultDeliveryService
	{
		public List<Drone> Drones { get; set; } = new List<Drone>();
		public bool Success;
		public string? Message;
	}
}