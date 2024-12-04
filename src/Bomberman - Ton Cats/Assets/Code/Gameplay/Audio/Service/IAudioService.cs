using Infrastructure;
using UnityEngine;

namespace Gameplay.Audio.Service
{
	public interface IAudioService : IService
	{
		void AssignMixerGroup(AudioSource audioSource, MixerGroup groupType);
		void AssignMusicClip(AudioSource audioSource, MusicType musicType);
	}
}