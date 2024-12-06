﻿using UnityEngine;

namespace Gameplay.Audio.Player
{
	public interface IAudioPlayer
	{
		void Play(ShortMusic key, AudioSource audioSource);

		void PlaySfx(string clipId, AudioSource audioSource,
			bool forceReplay = false);
	}
}