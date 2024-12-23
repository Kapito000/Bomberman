using Gameplay.Windows;
using Infrastructure.FinishLevel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.Feature.GameUI.Window
{
	public sealed class LaunchNextLevelWindow : BaseWindow
	{
		[SerializeField] Button _button;

		[Inject] IFinishLevelService _finishLevelService;

		public override WindowId Id => WindowId.LaunchNextLevel;

		protected override void Initialize()
		{
			_button.onClick.AddListener(ButtonClick);
		}

		protected override void OnCleanup()
		{
			_button.onClick.RemoveListener(ButtonClick);
		}

		void ButtonClick()
		{
			_finishLevelService.LaunchNextLevel();
		}
	}
}