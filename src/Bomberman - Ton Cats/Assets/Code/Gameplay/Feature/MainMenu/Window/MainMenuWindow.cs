using Gameplay.MainMenu;
using Gameplay.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.Feature.MainMenu.Window
{
	public sealed class MainMenuWindow : BaseWindow
	{
		[Inject] IMainMenuService _mainMenuService;

		[SerializeField] Button _launchGameButton;

		public override WindowId Id => WindowId.MainMenu;

		protected override void Initialize()
		{
			_launchGameButton.onClick.AddListener(OnLaunchGameButtonClick);
		}

		protected override void OnCleanup()
		{
			_launchGameButton.onClick.RemoveListener(OnLaunchGameButtonClick);
		}

		void OnLaunchGameButtonClick()
		{
			_mainMenuService.LaunchGame();
		}
	}
}