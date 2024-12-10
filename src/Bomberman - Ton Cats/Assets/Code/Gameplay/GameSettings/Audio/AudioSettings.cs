using Gameplay.Audio;
using Gameplay.Audio.MixerGroupProvider;
using Gameplay.GameSettings.StaticData;
using UnityEngine;
using Zenject;

namespace Gameplay.GameSettings.Audio
{
	public sealed class AudioSettings : IAudioSetting
	{
		readonly VolumeType[] _availableVolumes;

		[Inject] IAudioMixerProvider _mixerProvider;

		public AudioSettings()
		{
			_availableVolumes = CreateAvailableVolumes();
		}

		[Inject]
		void Construct(IGameSettingsStartValueData startValueData)
		{
			InitVolume(startValueData);
		}

		public void SetVolume(VolumeType volumeType, float value)
		{
			if (TryGetMixerParameter(volumeType, out var mixerParameter) == false)
				return;

			SetMixerVolume(value, mixerParameter);
		}

		public bool TryGetVolumeValue(VolumeType volumeType, out float value)
		{
			if (TryGetMixerParameter(volumeType, out var parameter) == false ||
			    TryGetMixerValue(parameter, out var volume) == false)
			{
				value = default;
				return false;
			}

			var maxVolume = Constant.Value.c_MixerMaxVolume;
			var minVolume = Constant.Value.c_MixerMinVolume;
			value = ConvertToValue(volume, minVolume, maxVolume);
			return true;
		}

		void SetMixerVolume(float value, string parameter)
		{
			var maxVolume = Constant.Value.c_MixerMaxVolume;
			var minVolume = Constant.Value.c_MixerMinVolume;
			var volume = ConvertToMixerVolume(value, minVolume, maxVolume);
			TrySetMixerValue(parameter, volume);
		}

		float ConvertToMixerVolume(float value, float minVolume, float maxVolume) =>
			Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f))
			* (maxVolume - minVolume) / 4f + maxVolume;

		float ConvertToValue(float volume, float minVolume, float maxVolume) =>
			Mathf.Pow(10, 4 * (volume - maxVolume) / (maxVolume - minVolume));

		bool TryGetMixerValue(string parameter, out float value)
		{
			if (_mixerProvider.Mixer.GetFloat(parameter, out value))
				return true;

			Debug.LogError(
				$"Cannot to get mixer volume by the \"{parameter}\" parameter.");
			return false;
		}

		bool TrySetMixerValue(string parameter, float value)
		{
			if (_mixerProvider.Mixer.SetFloat(parameter, value))
				return true;

			Debug.LogError(
				$"Cannot to set mixer volume by the \"{parameter}\" parameter.");
			return false;
		}

		void InitVolume(IGameSettingsStartValueData startValueData)
		{
			foreach (var volumeType in _availableVolumes)
			{
				if (!TryGetVolumeStartValue(startValueData, volumeType, out var value))
					continue;
				SetVolume(volumeType, value);
			}
		}

		static VolumeType[] CreateAvailableVolumes() =>
			new[]
			{
				VolumeType.Main,
				VolumeType.UI,
				VolumeType.SFX,
				VolumeType.Music,
			};

		static bool TryGetVolumeStartValue(
			IGameSettingsStartValueData startValueData, VolumeType volumeType,
			out float value)
		{
			if (startValueData.Audio.TryGetVolume(volumeType, out value) == false)
			{
				Debug.LogError(
					$"Cannot access to the volume start value: \"{volumeType}\".");
				return false;
			}
			return true;
		}

		bool TryGetMixerParameter(VolumeType volumeType, out string mixerParameter)
		{
			switch (volumeType)
			{
				case VolumeType.Main:
					mixerParameter = Constant.AudioMixerParameter.c_MainVolume;
					break;
				case VolumeType.UI:
					mixerParameter = Constant.AudioMixerParameter.c_UiVolume;
					break;
				case VolumeType.SFX:
					mixerParameter = Constant.AudioMixerParameter.c_SfxVolume;
					break;
				case VolumeType.Music:
					mixerParameter = Constant.AudioMixerParameter.c_MusicVolume;
					break;
				default:
					Debug.LogError($"Unknown {nameof(VolumeType)}: \"{volumeType}\".");
					mixerParameter = default;
					return false;
			}
			return true;
		}
	}
}