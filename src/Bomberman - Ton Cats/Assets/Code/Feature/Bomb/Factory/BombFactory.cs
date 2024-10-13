using Extensions;
using Factory.Kit;
using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Feature.Bomb.Factory
{
	public sealed class BombFactory : IBombFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _bombParent;

		public int CreateBomb(Vector2 pos, Transform parent)
		{
			var prefab = _kit.AssetProvider.Bomb();
			var instance = _kit.InstantiateService.Instantiate(prefab, pos, parent);
			var entity = _kit.EntityBehaviourFactory.CreateEntityBehaviour(instance);
			return entity;
		}

		public int CreateBombParent()
		{
			var instance = new GameObject("Bombs");
			var entity = _kit.EntityBehaviourFactory.CreateEntityBehaviour(instance);
			_bombParent.SetEntity(entity);
			_bombParent
				.Add<BombParent>()
				.Add<Common.Transform>().With(e => e.SetTransform(instance.transform))
				;
			return entity;
		}
	}
}