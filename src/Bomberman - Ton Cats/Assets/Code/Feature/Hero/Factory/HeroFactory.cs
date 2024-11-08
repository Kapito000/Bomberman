using Common.Component;
using Extensions;
using Factory.Kit;
using Feature.Bomb.Component;
using Feature.Hero.Behaviour;
using Feature.Hero.Component;
using Feature.Hero.StaticData;
using Feature.Input.Component;
using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Feature.Hero.Factory
{
	public sealed class HeroFactory : IHeroFactory
	{
		[Inject] IHeroData _heroData;
		[Inject] IFactoryKit _factoryKit;
		[Inject] EntityWrapper _heroEntity;

		public int CreateHero(Vector2 pos, Quaternion rot, Transform parent)
		{
			var prefab = _factoryKit.AssetProvider.Hero();
			var heroObj = _factoryKit.InstantiateService
				.Instantiate(prefab, pos, rot, parent);
			var entity = _factoryKit.EntityBehaviourFactory
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
	}
}