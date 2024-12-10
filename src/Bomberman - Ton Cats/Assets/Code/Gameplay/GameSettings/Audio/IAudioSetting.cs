using Gameplay.Audio;

namespace Gameplay.GameSettings.Audio
{
	public interface IAudioSetting
	{
		void SetVolume(VolumeType volumeType, float value);
	}
}