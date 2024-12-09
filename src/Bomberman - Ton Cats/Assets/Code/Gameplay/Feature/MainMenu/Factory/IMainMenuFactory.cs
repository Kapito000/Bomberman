using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.MainMenu.Factory
{
	public interface IMainMenuFactory : IFactory
	{
		int CreateUpperPanel(Transform parent);
		int CreateAmbientMusic(Transform parent);
	}
}