﻿using Extensions;
using Factory.Kit;
using Feature.Bomb.Component;
using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Feature.Bomb.Factory
{
	public sealed class BombFactory : IBombFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _bomb;
		[Inject] EntityWrapper _bombParent;

		public int CreateBomb(Vector2 pos, Transform parent)
		{
			var prefab = _kit.AssetProvider.Bomb();
			var instance = _kit.InstantiateService.Instantiate(prefab, pos, parent);
			var entity = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_bomb.SetEntity(entity);
			_bomb
				.Add<Component.Bomb>()
				;
			return entity;
		}

		public int CreateBombParent()
		{
			var instance = new GameObject("Bombs");
			var entity = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_bombParent.SetEntity(entity);
			_bombParent
				.Add<BombParent>()
				.Add<Common.Component.TransformComponent>().With(e => e.SetTransform(instance.transform))
				;
			return entity;
		}
	}
}