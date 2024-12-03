using Gameplay.Feature.Music.Factory;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.Music.System
{
	public sealed class CreateMusicSystem : IEcsRunSystem
	{
		[Inject] IMusicFactory _musicFactory;

		public void Run(IEcsSystems systems)
		{
			_musicFactory.CreateGameMusic();
		}
	}
}