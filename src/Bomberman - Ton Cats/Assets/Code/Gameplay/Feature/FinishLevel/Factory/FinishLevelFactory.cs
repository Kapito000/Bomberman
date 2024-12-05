using Gameplay.Audio;
using Gameplay.Audio.Service;
using Gameplay.Feature.FinishLevel.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.FinishLevel.Factory
{
	public sealed class FinishLevelFactory : IFinishLevelFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _entity;
		[Inject] IAudioService _audioService;

		public int CreateFinishLevelObserver()
		{
			return _entity.NewEntity()
				.Add<FinishLevelObserver>()
				.Enity;
		}

		public int CreateFinishLevelDoorEntity(Vector2Int cell)
		{
			_entity.NewEntity()
				.Add<FinishLevelDoor>()
				.AddCellPos(cell)
				;
			return _entity.Enity;
		}

		public int CreateFinishLevelMusic(Transform parent)
		{
			var prefab = _kit.AssetProvider.FinishLevelMusic();
			var instance = _kit.InstantiateService.Instantiate(prefab, parent);
			var audioSource = AdjustAudioSource(instance);
			
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_entity.SetEntity(e);
			_entity
				.Add<FinishLevelMusic>()
				.AddAudioSource(audioSource)
				;
			return e;
		}

		public GameObject CreateFinishLevelDoor(int doorEntity, Vector2 pos)
		{
			var prefab = _kit.AssetProvider.FinishLevelDoor();
			var instance = _kit.InstantiateService.Instantiate(prefab, pos);
			_kit.EntityBehaviourFactory.BindTogether(doorEntity, instance);
			_entity.SetEntity(doorEntity);
			_entity
				.AddTransform(instance.transform)
				;
			return instance;
		}

		AudioSource AdjustAudioSource(GameObject instance)
		{
			var audioSource = _audioService.ReplaceAudioSource(instance);
			_audioService.AssignMixerGroup(MixerGroup.Music, audioSource);
			_audioService.EstablishCommonSettings(audioSource);
			return audioSource;
		}
	}
}