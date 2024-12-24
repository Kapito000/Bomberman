using Common.Component;
using Extensions;
using Gameplay.Audio.Service;
using Gameplay.Feature.Bomb.Component;
using Gameplay.Feature.Hero.Component;
using Gameplay.Feature.Hero.StaticData;
using Gameplay.Feature.Input.Component;
using Gameplay.Feature.Life.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Hero.Factory
{
	public sealed class HeroFactory : IHeroFactory
	{
		[Inject] IHeroData _heroData;
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _heroEntity;
		[Inject] IAudioService _audioService;

		public int CreateHero(Vector2 pos, Quaternion rot, Transform parent)
		{
			var prefab = _kit.AssetProvider.Hero();
			var heroObj = _kit.InstantiateService
				.Instantiate(prefab, pos, rot, parent);
			var entity = _kit.EntityBehaviourFactory
				.InitEntityBehaviour(heroObj);
			_heroEntity.SetEntity(entity);

			_heroEntity
				.Add<HeroComponent>()
				.Add<InputReader>()
				.Add<CharacterInput>()
				.Add<MovementDirection>()
				.Add<BombCarrier>()
				.AddBombStack(4)
				.Add<MoveSpeed>().With(e => e.SetMoveSpeed(_heroData.MovementSpeed))
				.Add<LifePoints>().With(e =>
					e.SetLifePoints(_heroData.LifePointsOnStart))
				;

			AddTakenDamageEffectComponents(_heroEntity);

			return entity;
		}

		public int CreateHeroSpawnPoint(Vector2 pos)
		{
			var prefab = _kit.AssetProvider.HeroSpawnPoint();
			var instance = _kit.InstantiateService.Instantiate(prefab, pos);
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			return e;
		}

		void AddTakenDamageEffectComponents(EntityWrapper heroEntity)
		{
			var name = Constant.ObjectName.c_DamageAudioEffect;
			if (_audioService.TryCreateAdditionalAudioSource(heroEntity.Enity,
				    out var takenDamageEffectAudioSource, name: name) == false)
			{
				CastCannotInitCorrectlyErrorMessage();
				return;
			}

			heroEntity
				.AddTakenDamageAudioEffectId(Constant.AudioClipId.c_HeroTakenDamage)
				.AddTakenDamageEffectAudioSource(takenDamageEffectAudioSource)
				;
		}

		static void CastCannotInitCorrectlyErrorMessage() =>
			Debug.LogError("The hero cannot be initialized correctly.");
	}
}