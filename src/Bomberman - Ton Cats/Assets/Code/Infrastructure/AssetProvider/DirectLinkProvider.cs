﻿using Cinemachine;
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
		[SerializeField] GameObject _hero;
		[SerializeField] GameObject _bomb;
		[SerializeField] GameObject _explosion;
		[Header("UI")]
		[SerializeField] Canvas _root;
		[SerializeField] EventSystem _eventSystem;
		[SerializeField] Canvas _hudRoot;
		[SerializeField] GameObject _characterJoystick;
		[SerializeField] GameObject _putBombButton;

		public Camera Camera() => _camera;
		public CinemachineVirtualCamera VirtualCamera() => _virtualCamera;
		public GameObject Hero() => _hero;
		public GameObject Bomb() => _bomb;
		public Canvas UiRoot() => _root;
		public Canvas HudRoot() => _hudRoot;
		public GameObject CharacterJoystick() => _characterJoystick;
		public GameObject PutBombButton() => _putBombButton;
		public EventSystem EventSystem() => _eventSystem;
		public GameObject Explosion() => _explosion;
	}
}