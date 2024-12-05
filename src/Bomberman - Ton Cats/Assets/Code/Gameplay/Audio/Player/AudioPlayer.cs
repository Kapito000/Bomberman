using Gameplay.Audio.ClipProvider;
using UnityEngine;
using Zenject;

namespace Gameplay.Audio.Player
{
	public sealed class AudioPlayer : IAudioPlayer
	{
		[Inject] IAudioClipProvider _clipProvider;

		public void Play(ShortMusic key, AudioSource audioSource)
		{
			if (_clipProvider.TryGetWithDebug(key, out var clip))
			{
				audioSource.clip = clip;
				audioSource.Play();
			}
		}
	}
}