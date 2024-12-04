using Gameplay.Audio.MixerProvider;
using UnityEngine;
using Zenject;

namespace Gameplay.Audio.Service
{
	public sealed class AudioService : IAudioService
	{
		[Inject] IAudioMixerGroupProvider _mixerGroupProvider;
		
		public void AssignMixerGroup(AudioSource audioSource, MixerGroup mixerGroup)
		{
			var group = _mixerGroupProvider.MixerGroup(mixerGroup);
			audioSource.outputAudioMixerGroup = group;
		}
	}
}