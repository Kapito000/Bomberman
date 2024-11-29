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

		public int CreateFinishLevelObserver()
		{
			return _entity.NewEntity()
				.Add<FinishLevelObserver>()
				.Enity;
		}

		public int CreateFinishLevelDoor(Vector2 pos)
		{
			var prefab = _kit.AssetProvider.FinishLevelDoor();
			var instance = _kit.InstantiateService.Instantiate(prefab, pos);
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_entity.SetEntity(e);
			_entity
				.Add<FinishLevelDoor>()
				;
			return e;
		}
	}
}