using Gameplay.Audio.ClipProvider;
using Gameplay.Audio.MixerGroupProvider;
using UnityEngine;
using Zenject;

namespace Gameplay.Audio.Service
{
	public sealed class AudioService : IAudioService
	{
		[Inject] IAudioClipProvider _clipProvider;
		[Inject] IAudioMixerProvider _mixerProvider;

		public void AssignMixerGroup(AudioSource audioSource, MixerGroup groupType)
		{
			if (_mixerProvider.TryGetMixerGroup(groupType, out var group))
				audioSource.outputAudioMixerGroup = group;
			else
				Debug.LogError($"Cannot to get \"{groupType}\" group.");
		}

		public void AssignMusicClip(AudioSource audioSource, AmbientMusic ambientMusic)
		{
			audioSource.clip = _clipProvider.Musics[ambientMusic];
		}

		public AudioSource ReplaceAudioSource(GameObject instance)
		{
			var audioSource = instance.GetComponent<AudioSource>();
			if (audioSource == null)
				audioSource = instance.AddComponent<AudioSource>();
			return audioSource;
		}

		public void EstablishCommonSettings(AudioSource audioSource)
		{
			audioSource.playOnAwake = false;
		}
	}
}