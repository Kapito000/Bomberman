using Infrastructure;

namespace TimeService
{
	public interface ITimeService : IService
	{
		float DeltaTime();
	}
}