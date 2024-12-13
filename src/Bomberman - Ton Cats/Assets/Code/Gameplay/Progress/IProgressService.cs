using Infrastructure;

namespace Gameplay.Progress
{
	public interface IProgressService : IService
	{
		int ReachedLevel { get; }
	}
}