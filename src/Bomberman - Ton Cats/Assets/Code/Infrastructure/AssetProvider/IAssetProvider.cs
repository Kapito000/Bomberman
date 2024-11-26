using Cinemachine;
using Gameplay.Feature.HUD.Behaviour;
using Gameplay.Windows;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Infrastructure.AssetProvider
{
	public interface IAssetProvider : IService
	{
		Camera Camera();
		CinemachineVirtualCamera VirtualCamera();
		GameObject Hero();
		GameObject HeroSpawnPoint();
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
		GameObject BaseEnemy();
		GameObject DestructibleTile();
	}
}