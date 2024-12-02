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

		public int CreateFinishLevelDoorEntity(Vector2Int cell)
		{
			_entity.NewEntity()
				.Add<FinishLevelDoor>()
				.AddCellPos(cell)
				;
			return _entity.Enity;
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
	}
}