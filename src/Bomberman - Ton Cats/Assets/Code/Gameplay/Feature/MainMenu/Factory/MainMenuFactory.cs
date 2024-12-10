using Gameplay.Audio;
using Gameplay.Audio.Factory;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.MainMenu.Factory
{
	public sealed class MainMenuFactory : IMainMenuFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _entity;
		[Inject] IMusicFactory _musicFactory;

		public int CreateAmbientMusic(Transform parent)
		{
			var prefab = _kit.AssetProvider.GameMusic();
			return _musicFactory
				.CreateAmbientMusic(AmbientMusic.MainMenu, prefab, parent);
		}
	}
}