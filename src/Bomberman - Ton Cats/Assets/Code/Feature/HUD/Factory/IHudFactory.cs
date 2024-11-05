using Factory;
using UnityEngine;

namespace Feature.HUD.Factory
{
	public interface IHudFactory : IFactory
	{
		int CreateHudRoot(Transform parent);
		int CreateCharacterJoystick(Transform parent);
		int CreatePutBombButton(Transform parent);
		int CreateUpperPanel(Transform parent);
	}
}