using Factory;
using UnityEngine;

namespace Feature.HUD.Factory
{
	public interface IHudFactory : IFactory
	{
		int CreateHudRoot(Transform parent);
		int CharacterJoystick(Transform parent);
	}
}