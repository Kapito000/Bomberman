using Factory;
using UnityEngine;

namespace Feature.Bomb.Factory
{
	public interface IExplosionFactory : IFactory
	{
		int CreateExplosionRequest(Vector2 pos);

		int CreateExplosionPart(Vector2 pos, Vector2 direction, Transform parent,
			ExplosionPart part);

		int CreateExplosionCenter(Vector2 pos, Transform parent);
	}

	public enum ExplosionPart
	{
		Center,
		Middle,
		End,
	}
}