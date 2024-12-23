using Gameplay.Progress;
using Infrastructure.ECS;
using Zenject;

namespace Gameplay.Feature.PlayerProgress.Factory
{
	public sealed class PlayerProgressFactory : IPlayerProgressFactory
	{
		[Inject] EntityWrapper _wrapper;
		[Inject] IProgressService _progressService;

		public int CreatePlayerProgress()
		{
			return _wrapper.NewEntity()
				.Add<Component.PlayerProgress>()
				.AddReachedLevel(_progressService.ReachedLevel)
				.Enity;
		}
	}
}