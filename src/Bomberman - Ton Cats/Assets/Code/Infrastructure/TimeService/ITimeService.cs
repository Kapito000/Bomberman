namespace Infrastructure.TimeService
{
	public interface ITimeService : IService
	{
		float DeltaTime();
		void Stop();
		void Run();
	}
}