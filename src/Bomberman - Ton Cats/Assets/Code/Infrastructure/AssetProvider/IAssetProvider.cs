using Cinemachine;
using Feature.Bomb.Factory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Infrastructure.AssetProvider
{
	public interface IAssetProvider : IService
	{
		Camera Camera();
		CinemachineVirtualCamera VirtualCamera();
		GameObject Hero();
		GameObject Bomb();
		Canvas UiRoot();
		Canvas HudRoot();
		GameObject CharacterJoystick();
		EventSystem EventSystem();
		GameObject Explosion();
	}
}