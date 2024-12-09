using Gameplay.Audio;
using Gameplay.Audio.Service;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.UI
{
	public class ButtonClickSound : MonoBehaviour
	{
		[Inject] IAudioService _audioService;

		[SerializeField] Button _button;
		[SerializeField] AudioSource _audioSource;

		void Awake()
		{
			_button.onClick.AddListener(OnButtonClick);
			_audioService.AssignUiSfxClip(UiSfx.ButtonClick, _audioSource);
			_audioService.AssignMixerGroup(MixerGroup.UI, _audioSource);
		}

		void OnButtonClick()
		{
			_audioSource.Play();
		}
	}
}