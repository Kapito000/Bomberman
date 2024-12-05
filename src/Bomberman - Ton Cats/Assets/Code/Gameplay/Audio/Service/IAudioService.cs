using Infrastructure;
using UnityEngine;

namespace Gameplay.Audio.Service
{
	public interface IAudioService : IService
	{
		void AssignMusicClip(AudioSource audioSource, AmbientMusic ambientMusic);
		void AssignMixerGroup(AudioSource audioSource, MixerGroup groupType);
		void EstablishCommonSettings(AudioSource audioSource);
		AudioSource ReplaceAudioSource(GameObject instance);
	}
}