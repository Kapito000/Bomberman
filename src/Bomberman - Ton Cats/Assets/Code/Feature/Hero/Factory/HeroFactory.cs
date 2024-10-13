using Common;
using Extensions;
using Factory.Kit;
using Feature.Bomb;
using Feature.Input;
using Infrastructure.ECS;
using StaticData.Hero;
using UnityEngine;
using Zenject;
using Transform = UnityEngine.Transform;

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
				.CreateEntityBehaviour(heroObj);
			_heroEntity.SetEntity(entity);

			_heroEntity
				.Add<Hero>()
				.Add<InputReader>()
				.Add<CharacterInput>()
				.Add<MovementDirection>()
				.Add<BombCarrier>()
				.Add<BombNumber>().With(e=> e.SetBombNumber(_heroData.StartBombNumber))
				.Add<MoveSpeed>().With(e => e.SetMoveSpeed(_heroData.MovementSpeed))
				;

			return entity;
		}
	}
}