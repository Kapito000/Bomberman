using Gameplay.Audio;
using Gameplay.Audio.Service;
using Gameplay.Feature.Music.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Music.Factory
{
	public sealed class MusicFactory : IMusicFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _entity;
		[Inject] IAudioService _audioService;

		public int CreateGameMusic()
		{
			var prefab = _kit.AssetProvider.GameMusicPrefab();
			var instance = _kit.InstantiateService.Instantiate(prefab);
			var audioSource = GetAudioSource(instance);
			
			AdjustAudioSource(audioSource);

			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_entity.SetEntity(e);
			_entity
				.Add<MusicComponent>()
				.AddAudioSource(audioSource)
				;
			return e;
		}

		AudioSource GetAudioSource(GameObject instance)
		{
			var audioSource = instance.GetComponent<AudioSource>();
			if (audioSource == null)
			{
				Debug.LogWarning(
					$"Music prefab has no \"{nameof(AudioSource)}\" component.");
				audioSource = instance.AddComponent<AudioSource>();
			}
			return audioSource;
		}

		void AdjustAudioSource(AudioSource audioSource)
		{
			var mixerGroup = _audioService.MixerGroup(MixerGroup.Music);
			audioSource.outputAudioMixerGroup = mixerGroup;
			
			audioSource.playOnAwake = false;
		}
	}
}