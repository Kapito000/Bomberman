using Common.Component;
using Extensions;
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
				.Add<BombNumber>().With(e => e.SetBombNumber(_heroData.StartBombNumber))
				.Add<MoveSpeed>().With(e => e.SetMoveSpeed(_heroData.MovementSpeed))
				.Add<LifePoints>().With(e => e.SetLifePoints(_heroData.LifePointsOnStart))
				;

			return entity;
		}

		public int CreateHeroSpawnPoint(Vector2 pos)
		{
			var prefab = _kit.AssetProvider.HeroSpawnPoint();
			var instance = _kit.InstantiateService.Instantiate(prefab, pos);
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			return e;
		}
	}
}