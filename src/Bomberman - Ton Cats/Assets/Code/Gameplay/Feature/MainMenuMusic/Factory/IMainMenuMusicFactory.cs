using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.MainMenuMusic.Factory
{
	public interface IMainMenuMusicFactory : IFactory
	{
		int CreateAmbientMusic(Transform parent);
	}
}