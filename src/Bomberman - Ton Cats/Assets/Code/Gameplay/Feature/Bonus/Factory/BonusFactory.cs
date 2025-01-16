using Common.Component;
using Gameplay.Feature.Bonus.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Bonus.Factory
{
	public sealed class BonusFactory : IBonusFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _entity;

		public int CreateBonusEntity(string bonusType, Vector2Int cell)
		{
			_entity.NewEntity()
				.Add<BonusComponent>()
				.AddBonusType(bonusType)
				.AddCellPos(cell)
				.Add<FirstBreath>()
				;
			return _entity.Enity;
		}

		public GameObject CreateBonusObject(Vector2 pos, int bonusEntity)
		{
			var prefab = _kit.AssetProvider.Bonus();
			var instance = _kit.InstantiateService.Instantiate(prefab, pos);
			_kit.EntityBehaviourFactory.BindTogether(bonusEntity, instance);
			_entity.SetEntity(bonusEntity);
			_entity
				.AddTransform(instance.transform)
				;

			return instance;
		}
	}
}