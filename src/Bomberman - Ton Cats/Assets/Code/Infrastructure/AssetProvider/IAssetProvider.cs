using System.Collections.Generic;
using Cinemachine;
using Gameplay.Feature.HUD.Feature.Bomb.Behaviour;
using Gameplay.Feature.HUD.Feature.Life.Behaviour;
using Gameplay.Feature.HUD.Feature.Timer.Behaviour;
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
		GameObject WindowsRoot();
		GameTimerDisplay GameTimerDisplay();
		GameObject FinishLevelDoor();
		Dictionary<WindowId, BaseWindow> AllWindows { get; }
		GameObject GameMusicPrefab();
	}
}