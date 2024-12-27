using System.Collections.Generic;
using Cinemachine;
using Common.HUD;
using Gameplay.Feature.Bomb;
using Gameplay.Feature.HUD.Feature.Timer.Behaviour;
using Gameplay.Windows;
using UnityEngine;
using UnityEngine.EventSystems;
using Menu = Constant.CreateAssetMenu;

namespace Infrastructure.AssetProvider
{
	[CreateAssetMenu(
		menuName = Menu.Path.c_AssetProvider + nameof(DirectLinkProvider),
		fileName = nameof(DirectLinkProvider))]
	public sealed class DirectLinkProvider : ScriptableObject, IAssetProvider
	{
		[SerializeField] Camera _camera;
		[SerializeField] CinemachineVirtualCamera _virtualCamera;
		[Space]
		[SerializeField] GameObject _hero;
		[SerializeField] GameObject _heroSpawnPoint;
		[SerializeField] GameObject _explosion;
		[SerializeField] GameObject _baseEnemy;
		[SerializeField] GameObject _finishLevelDoor;
		[Header("Bombs")]
		[SerializeField] GameObject _bomb;
		[SerializeField] GameObject _bombHunter;
		[Header("Audio")]
		[SerializeField] GameObject _ambientMusic;
		[SerializeField] GameObject _finishLevelMusic;
		[Header("Tile prefabs")]
		[SerializeField] GameObject _destructibleTile;
		[Header("UI")]
		[SerializeField] Canvas _root;
		[SerializeField] GameObject _windowsRoot;
		[SerializeField] GameObject _mainMenuUpperPanel;
		[SerializeField] EventSystem _eventSystem;
		[SerializeField] WindowsDictionary _windows;
		[Header("HUD")]
		[SerializeField] Canvas _hudRoot;
		[SerializeField] GameObject _upperPanel;
		[SerializeField] GameObject _putBombButton;
		[SerializeField] GameObject _characterJoystick;
		[SerializeField] IntegerDisplay _lifePointsPanel;
		[SerializeField] IntegerDisplay _bombCounterPanel;
		[SerializeField] IntegerDisplay _enemyCounterDisplay;
		[SerializeField] GameTimerDisplay _gameTimerDisplay;

		public GameObject Bomb(BombType bombType) =>
			bombType == BombType.Hunter ? _bombHunter : _bomb;

		public Camera Camera() => _camera;
		public Canvas UiRoot() => _root;
		public Canvas HudRoot() => _hudRoot;
		public GameObject Hero() => _hero;
		public GameObject BaseEnemy() => _baseEnemy;
		public GameObject Explosion() => _explosion;
		public GameObject UpperPanel() => _upperPanel;
		public GameObject WindowsRoot() => _windowsRoot;
		public GameObject PutBombButton() => _putBombButton;
		public GameObject HeroSpawnPoint() => _heroSpawnPoint;
		public GameObject FinishLevelDoor() => _finishLevelDoor;
		public GameObject DestructibleTile() => _destructibleTile;
		public GameObject CharacterJoystick() => _characterJoystick;
		public BaseWindow WindowPrefab(WindowId id) => _windows[id];
		public EventSystem EventSystem() => _eventSystem;
		public IntegerDisplay LifePointsPanel() => _lifePointsPanel;
		public GameTimerDisplay GameTimerDisplay() => _gameTimerDisplay;
		public IntegerDisplay BombCounterPanel() => _bombCounterPanel;
		public CinemachineVirtualCamera VirtualCamera() => _virtualCamera;
		public Dictionary<WindowId, BaseWindow> AllWindows => _windows;
		public GameObject GameMusic() => _ambientMusic;
		public GameObject FinishLevelMusic() => _finishLevelMusic;
		public GameObject MainMenuUpperPanel() => _mainMenuUpperPanel;
		public IntegerDisplay EnemyCounterDisplay() => _enemyCounterDisplay;
	}
}