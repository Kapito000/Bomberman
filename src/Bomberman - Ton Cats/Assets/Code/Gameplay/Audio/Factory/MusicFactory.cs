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

		public int CreateAmbientMusic(AmbientMusic musicType, GameObject prefab,
			Transform parent)
		{
			var instance = _kit.InstantiateService.Instantiate(prefab, parent);
			var audioSource = _audioService.ReplaceAudioSource(instance);

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

		void AdjustAudioSource(AudioSource audioSource, AmbientMusic musicType)
		{
			_audioService.AssignMusicClip(musicType, audioSource);
			_audioService.AssignMixerGroup(MixerGroup.Music, audioSource);
			_audioService.EstablishCommonSettings(audioSource);
		}
	}
}