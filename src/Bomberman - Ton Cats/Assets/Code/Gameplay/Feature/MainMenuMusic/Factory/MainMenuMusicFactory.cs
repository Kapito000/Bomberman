using Gameplay.Audio;
using Gameplay.Audio.Factory;
using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.MainMenuMusic.Factory
{
	public sealed class MainMenuMusicFactory : IMainMenuMusicFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] IMusicFactory _musicFactory;

		public int CreateAmbientMusic(Transform parent)
		{
			var prefab = _kit.AssetProvider.GameMusic();
			return _musicFactory
				.CreateAmbientMusic(AmbientMusic.MainMenu, prefab, parent);
		}
	}
}