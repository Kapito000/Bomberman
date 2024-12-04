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

		public void AssignMusicClip(AudioSource audioSource, MusicType musicType)
		{
			audioSource.clip = _clipProvider.Musics[musicType];
		}
	}
}