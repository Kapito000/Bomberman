using Gameplay.Feature.PlayerProgress.Factory;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.PlayerProgress.System
{
	public sealed class CreatePlayerProgrressSystem : IEcsRunSystem
	{
		[Inject] IPlayerProgressFactory _factory;

		public void Run(IEcsSystems systems)
		{
			_factory.CreatePlayerProgress();
		}
	}
}