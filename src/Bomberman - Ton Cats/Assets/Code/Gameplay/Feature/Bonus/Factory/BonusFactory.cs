using Gameplay.Feature.Bonus.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Bonus.Factory
{
	public sealed class BonusFactory : IBonusFactory
	{
		[Inject] IFactoryKit _factoryKit;
		[Inject] EntityWrapper _entity; 

		public int CreateBonusEntity(string bonusType, Vector2Int cell)
		{
			_entity.NewEntity()
				.Add<BonusComponent>()
				.AddBonusType(bonusType)
				.AddCellPos(cell)
				;
			return _entity.Enity;

		}
	}
}