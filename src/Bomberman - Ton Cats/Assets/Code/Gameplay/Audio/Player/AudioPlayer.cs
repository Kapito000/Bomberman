using Common.Component;
using Gameplay.Audio.ClipProvider;
using Gameplay.Feature.Audio.Behaviour;
using Gameplay.Feature.Audio.Component;
using Gameplay.LevelData;
using UnityEngine;
using Zenject;

namespace Gameplay.Audio.Player
{
	public sealed class AudioPlayer : IAudioPlayer
	{
		[Inject] IGameLevelData _levelData;
		[Inject] IAudioClipProvider _clipProvider;

		public void Play(ShortMusic key, AudioSource audioSource)
		{
			if (_clipProvider.TryGetWithDebug(key, out var clip))
			{
				audioSource.clip = clip;
				audioSource.Play();
			}
		}

		public void PlaySfx(string clipId, AudioSource audioSource,
			bool forceReplay)
		{
			if (forceReplay == false)
			{
				if (audioSource.isPlaying)
					return;
			}

			PlaySfx(clipId, audioSource);
		}

		public void PlayClipAtPoint(AudioClip clip, Vector2 pos)
		{
			if (PooledAudioSourcePool().SpawnAndTryGetEntity(out var entity) == false)
			{
				Debug.LogError("Cannot play clip at point.");
				return;
			}
			
			var wrapper = _levelData.NewEntityWrapper();
			wrapper.SetEntity(entity);
			if (wrapper.Has<TransformComponent, AudioSourceComponent>() == false)
			{
				Debug.LogError("The pooled audio source entity is not correct.");
				return;
			}
			
			wrapper.Transform().position = pos;
			var audioSource = wrapper.AudioSource();
			audioSource.clip = clip;
			audioSource.Play();
			wrapper.Add<EndPlayDespawn>();
		}

		public void PlaySfxClipAtPoint(string key, Vector2 pos)
		{
			if (_clipProvider.TryGetSfxWithDebug(key, out var clip))
				PlayClipAtPoint(clip, pos);
		}

		void PlaySfx(string clipId, AudioSource audioSource)
		{
			if (_clipProvider.TryGetSfxWithDebug(clipId, out var clip))
			{
				audioSource.clip = clip;
				audioSource.Play();
			}
		}

		PooledAudioSource.Pool PooledAudioSourcePool() =>
			_levelData.AudioSourcePool;
	}
}