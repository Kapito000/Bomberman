using Gameplay.Audio.ClipProvider;
using Gameplay.Audio.MixerGroupProvider;
using Gameplay.Audio.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Audio.Service
{
	public sealed class AudioService : IAudioService
	{
		[Inject] IAudioClipProvider _clipProvider;
		[Inject] IAudioMixerProvider _mixerProvider;

		[Inject] public IAudioPlayer Player { get; }

		public void AssignMusicClip(AmbientMusic key, AudioSource audioSource)
		{
			if (_clipProvider.TryGetWithDebug(key, out var clip)) 
				audioSource.clip = clip;
		}

		public void AssignMixerGroup(MixerGroup groupType, AudioSource audioSource)
		{
			if (_mixerProvider.TryGetMixerGroup(groupType, out var group))
				audioSource.outputAudioMixerGroup = group;
			else
				Debug.LogError($"Cannot to get \"{groupType}\" group.");
		}

		public void EstablishCommonSettings(AudioSource audioSource)
		{
			audioSource.playOnAwake = false;
		}

		public AudioSource ReplaceAudioSource(GameObject instance)
		{
			var audioSource = instance.GetComponent<AudioSource>();
			if (audioSource == null)
				audioSource = instance.AddComponent<AudioSource>();
			return audioSource;
		}
	}
}