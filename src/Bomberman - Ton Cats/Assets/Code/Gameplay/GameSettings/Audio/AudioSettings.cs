using Gameplay.Audio;
using Gameplay.Audio.MixerGroupProvider;
using UnityEngine;
using Zenject;

namespace Gameplay.GameSettings.Audio
{
	public sealed class AudioSettings : IAudioSetting
	{
		[Inject] IAudioMixerProvider _mixerProvider;

		public void SetVolume(VolumeType volumeType, float value)
		{
			switch (volumeType)
			{
				case VolumeType.Main:
					SetMixerVolume(value, Constant.AudioMixerParameter.c_MainVolume);
					break;
				case VolumeType.UI:
					SetMixerVolume(value, Constant.AudioMixerParameter.c_UiVolume);
					break;
				case VolumeType.SFX:
					SetMixerVolume(value, Constant.AudioMixerParameter.c_SfxVolume);
					break;
				case VolumeType.Music:
					SetMixerVolume(value, Constant.AudioMixerParameter.c_MusicVolume);
					break;
				default:
					Debug.LogError($"Unknown {nameof(VolumeType)}: \"{volumeType}\".");
					return;
			}
		}

		void SetMixerVolume(float value, string mixerParameter)
		{
			var maxVolume = Constant.Value.c_MixerMaxVolume;
			var minVolume = Constant.Value.c_MixerMinVolume;
			var volume = ConvertToMixerVolume(value, minVolume, maxVolume);
			_mixerProvider.Mixer.SetFloat(mixerParameter, volume);
		}

		float ConvertToMixerVolume(float value, float minVolume, float maxVolume) =>
			Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f))
			* (maxVolume - minVolume) / 4f + maxVolume;
	}
}