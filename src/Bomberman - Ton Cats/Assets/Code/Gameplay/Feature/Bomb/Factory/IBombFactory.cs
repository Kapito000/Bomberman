using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.Bomb.Factory
{
	public interface IBombFactory : IFactory
	{
		int CreateBomb(BombType bombType, Vector2Int cell, Transform parent);
		int CreateBombParent();
		int CreateCallExplosion(Vector2Int cell, int explosionRadius);
		int CreateExplosionPart(Vector2Int cell, ExplosionPart part, Transform parent,
			Vector2 direction = default);
		void CreateDestructibleTile(Vector2 pos, Transform parent);
	}
}