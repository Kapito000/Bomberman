using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.Bomb.Factory
{
	public interface IBombFactory : IFactory
	{
		int CreateBomb(Vector2 pos, Transform parent);
		int CreateBombParent();
	}
}