using Factory;
using UnityEngine;

namespace Feature.Bomb.Factory
{
	public interface IBombFactory : IFactory
	{
		int CreateBomb(Vector2 pos, Transform parent);
		int CreateBombParent();
	}
}