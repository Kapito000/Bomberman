using Gameplay.Windows;
using Infrastructure.FinishLevel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.Feature.GameUI.Window
{
	public class FinishLevelWindow : BaseWindow
	{
		[SerializeField] WindowId _windowId;
		[Space]
		[SerializeField] Button _goToMainMenuButton;

		[Inject] IFinishLevelService _finishLevelService;

		public override WindowId Id => _windowId;

		protected override void Initialize()
		{
			_goToMainMenuButton.onClick.AddListener(OnGoToMainMenuButtonClick);
		}

		protected override void OnCleanup()
		{
			_goToMainMenuButton.onClick.RemoveListener(OnGoToMainMenuButtonClick);
		}

		void OnGoToMainMenuButtonClick()
		{
			_finishLevelService.SwitchGameToMainMenu();
		}
	}
}