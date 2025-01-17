using Common.Component;
using Gameplay.Feature.Bonus.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace Gameplay.Feature.Bonus.Factory
{
	public sealed class BonusFactory : IBonusFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _entity;

		public int CreateBonusParent()
		{
			var go = new GameObject(Constant.ObjectName.c_BonusesParent);

			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(go);
			_entity.SetEntity(e);
			_entity
				.Add<BonusesParent>()
				.AddTransform(go.transform);
			;

			return e;
		}

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

		public GameObject CreateBonusObject(Vector2 pos, int bonusEntity,
			Transform parent)
		{
			var prefab = _kit.AssetProvider.Bonus();
			var instance = _kit.InstantiateService.Instantiate(prefab, pos, parent);
			_kit.EntityBehaviourFactory.BindTogether(bonusEntity, instance);
			_entity.SetEntity(bonusEntity);
			_entity
				.AddTransform(instance.transform)
				.Add<ObjectFirstBreath>()
				;

			return instance;
		}
	}
}