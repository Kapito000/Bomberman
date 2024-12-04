using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Audio.Factory
{
	public interface IMusicFactory : IFactory
	{
		int CreateAmbientMusic(AmbientMusic musicType, GameObject prefab);
	}
}