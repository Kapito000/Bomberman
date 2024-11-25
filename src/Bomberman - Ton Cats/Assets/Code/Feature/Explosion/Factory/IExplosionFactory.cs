using Factory;
using Feature.Bomb;
using UnityEngine;

namespace Feature.Explosion.Factory
{
	public interface IExplosionFactory : IFactory
	{
		int CreateExplosionRequest(Vector2 pos);
		int CreateExplosionPart(Vector2 pos, Vector2 direction, Transform parent,
			EExplosionPart part);
		int CreateExplosionCenter(Vector2 pos, Transform parent);
		void CreateDestructibleTile(Vector2 pos, Transform parent);
	}
}