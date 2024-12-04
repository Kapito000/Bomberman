using Gameplay.Audio.ClipProvider;
using Gameplay.Audio.Service;
using Gameplay.Feature.GameMusic.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Gameplay.Audio.Factory
{
	public sealed class MusicFactory : IMusicFactory
	{
		[Inject] EcsWorld _world;
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _entity;
		[Inject] IAudioService _audioService;
		[Inject] IAudioClipProvider _audioClipProvider;

		public int CreateAmbientMusic(AmbientMusic musicType, GameObject prefab)
		{
			var instance = _kit.InstantiateService.Instantiate(prefab);
			var audioSource = GetAudioSource(instance);

			AdjustAudioSource(audioSource, musicType);

			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_entity.SetEntity(e);
			_entity.SetEntity(e);
			_entity
				.Add<AmbientMusicComponent>()
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

		void AdjustAudioSource(AudioSource audioSource, AmbientMusic musicType)
		{
			_audioService.AssignMixerGroup(audioSource, MixerGroup.Music);
			audioSource.playOnAwake = false;
			_audioService.AssignMusicClip(audioSource, musicType);
		}
	}
}