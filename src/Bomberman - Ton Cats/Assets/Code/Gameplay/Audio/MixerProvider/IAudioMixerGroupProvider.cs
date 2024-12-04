using Infrastructure;
using UnityEngine.Audio;

namespace Gameplay.Audio.MixerProvider
{
	public interface IAudioMixerGroupProvider : IService
	{
		AudioMixer Mixer { get; }
		AudioMixerGroup MixerGroup(MixerGroup group);
	}
}