using Infrastructure;
using UnityEngine;

namespace Gameplay.Audio.Service
{
	public interface IAudioService : IService
	{
		void AssignMixerGroup(AudioSource audioSource, MixerGroup mixerGroup);
	}
}