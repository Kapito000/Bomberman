using Gameplay.Audio;
using Gameplay.StaticData;

namespace Gameplay.GameSettings.StaticData.Audio
{
	public interface IAudioStartValueData : IStaticData
	{
		bool TryGetVolume(VolumeType volumeType, out float value);
	}
}