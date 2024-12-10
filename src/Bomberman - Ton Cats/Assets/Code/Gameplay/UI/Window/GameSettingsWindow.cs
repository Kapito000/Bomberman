using Gameplay.Audio;
using Gameplay.GameSettings;
using Gameplay.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.UI.Window
{
	public class GameSettingsWindow : BaseWindow
	{
		[SerializeField] Slider _mainVolume;
		[SerializeField] Slider _uiVolume;
		[SerializeField] Slider _sfxVolume;
		[SerializeField] Slider _musicVolume;

		[Inject] IGameSettings _gameSettings;

		public override WindowId Id => WindowId.GameSettings;

		protected override void Initialize()
		{
			_mainVolume.onValueChanged.AddListener(OnMainVolumeChanged);
			_uiVolume.onValueChanged.AddListener(OnUiVolumeChanged);
			_sfxVolume.onValueChanged.AddListener(OnSfxVolumeChanged);
			_musicVolume.onValueChanged.AddListener(OnMusicVolumeChanged);
		}

		protected override void OnCleanup()
		{
			_mainVolume.onValueChanged.RemoveListener(OnMainVolumeChanged);
			_uiVolume.onValueChanged.RemoveListener(OnUiVolumeChanged);
			_sfxVolume.onValueChanged.RemoveListener(OnSfxVolumeChanged);
			_musicVolume.onValueChanged.RemoveListener(OnMusicVolumeChanged);
		}

		void OnMainVolumeChanged(float value)
		{
			_gameSettings.Audio.SetVolume(VolumeType.Main, value);
		}

		void OnUiVolumeChanged(float value)
		{
			_gameSettings.Audio.SetVolume(VolumeType.UI, value);
		}

		void OnSfxVolumeChanged(float value)
		{
			_gameSettings.Audio.SetVolume(VolumeType.SFX, value);
		}

		void OnMusicVolumeChanged(float value)
		{
			_gameSettings.Audio.SetVolume(VolumeType.Music, value);
		}
	}
}