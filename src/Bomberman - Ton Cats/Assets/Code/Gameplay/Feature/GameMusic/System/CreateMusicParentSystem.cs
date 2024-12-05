using Gameplay.Feature.GameMusic.Factory;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.GameMusic.System
{
	public sealed class CreateMusicParentSystem : IEcsRunSystem
	{
		[Inject] IGameMusicFactory _gameMusicFactory;
		
		public void Run(IEcsSystems systems)
		{
			_gameMusicFactory.CreateMusicParent();
		}
	}
}