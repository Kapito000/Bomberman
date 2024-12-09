using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.Explosion.Factory
{
	public interface IExplosionFactory : IFactory
	{
		int CreateExplosionRequest(Vector2 pos);
		int CreateExplosionPart(Vector2 pos, Vector2 direction, Transform parent,
			ExplosionPart part);
		int CreateExplosionCenter(Vector2 pos, Transform parent);
		void CreateDestructibleTile(Vector2 pos, Transform parent);
	}
}