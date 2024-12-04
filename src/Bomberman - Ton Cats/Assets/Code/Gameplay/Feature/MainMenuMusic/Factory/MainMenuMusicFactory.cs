using Gameplay.Audio;
using Gameplay.Audio.Factory;
using Infrastructure.Factory.Kit;
using Zenject;

namespace Gameplay.Feature.MainMenuMusic.Factory
{
	public sealed class MainMenuMusicFactory : IMainMenuMusicFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] IMusicFactory _musicFactory;

		public int CreateAmbientMusic()
		{
			var prefab = _kit.AssetProvider.GameMusicPrefab();
			return _musicFactory.CreateAmbientMusic(AmbientMusic.MainMenu, prefab);
		}
	}
}