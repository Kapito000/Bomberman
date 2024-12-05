using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.GameMusic.Factory
{
	public interface IGameMusicFactory : IFactory
	{
		int CreateGameMusic(Transform parent);
		int CreateMusicParent();
	}
}