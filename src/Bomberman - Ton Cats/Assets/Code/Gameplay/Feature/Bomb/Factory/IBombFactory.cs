﻿using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.Bomb.Factory
{
	public interface IBombFactory : IFactory
	{
		int CreateBomb(BombType bombType, Vector2 pos, Transform parent);
		int CreateBombParent();
		int CreateExplosionRequest(Vector2 pos, float explosionRadius);
		int CreateExplosionPart(Vector2 pos, Vector2 direction, Transform parent,
			ExplosionPart part);
		int CreateExplosionCenter(Vector2 pos, Transform parent);
		void CreateDestructibleTile(Vector2 pos, Transform parent);
	}
}