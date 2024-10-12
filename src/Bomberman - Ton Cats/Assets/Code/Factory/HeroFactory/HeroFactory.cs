using Common;
using Extensions;
using Feature.Hero;
using Feature.Input;
using Infrastructure.ECS;
using StaticData.Hero;
using UnityEngine;
using Zenject;
using Transform = UnityEngine.Transform;

namespace Factory.HeroFactory
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
				.Add<MoveSpeed>().With(e => e.SetMoveSpeed(_heroData.MovementSpeed))
				;

			return entity;
		}
	}
}