using Infrastructure;
using UnityEngine.Audio;

namespace Gameplay.Audio.Service
{
	public interface IAudioService : IService
	{
		public AudioMixer Mixer(MixerGroup mixerGroup);
		public AudioMixerGroup MixerGroup(MixerGroup mixerGroup);
	}
}