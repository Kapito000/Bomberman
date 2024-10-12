using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Infrastructure.AssetProvider
{
	public interface IAssetProvider : IService
	{
		Camera Camera();
		CinemachineVirtualCamera VirtualCamera();
		GameObject Hero();
		Canvas UiRoot();
		Canvas HudRoot();
		GameObject CharacterJoystick();
		EventSystem EventSystem();
	}
}