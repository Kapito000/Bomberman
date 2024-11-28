﻿using Gameplay.MainMenu;
using Gameplay.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.Feature.UI.Windows
{
	public sealed class MainMenuWindow : BaseWindow
	{
		[Inject] IMainMenuService _mainMenuService;

		[SerializeField] Button _launchGameButton;

		protected override void Initialize()
		{
			_launchGameButton.onClick.AddListener(OnLaunchGameButtonClick);
		}

		protected override void OnCleanup()
		{
			_launchGameButton.onClick.RemoveListener(OnLaunchGameButtonClick);
		}

		protected override void SetWindowId(ref WindowId id) =>
			id = WindowId.MainMenu;

		void OnLaunchGameButtonClick()
		{
			_mainMenuService.LaunchGame();
		}
	}
}