using Windows;
using Cinemachine;
using Feature.HUD.Behaviour;
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
		GameObject PutBombButton();
		EventSystem EventSystem();
		GameObject Explosion();
		GameObject UpperPanel();
		LifePointsPanel LifePointsPanel();
		BombCounterPanel BombCounterPanel();
		BaseWindow WindowPrefab(WindowId id);
	}
}