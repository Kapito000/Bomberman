using UnityEngine;

namespace Factory.HudFactory
{
	public interface IHudFactory : IFactory
	{
		int CreateHudRoot(Transform parent);
		int CharacterJoystick(Transform parent);
	}
}