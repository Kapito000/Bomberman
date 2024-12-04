using Infrastructure;
using UnityEngine.Audio;

namespace Gameplay.Audio.MixerGroupProvider
{
	public interface IAudioMixerProvider : IService
	{
		AudioMixer Mixer { get; }
		bool TryGetMixerGroup(MixerGroup groupType, out AudioMixerGroup group);
	}
}