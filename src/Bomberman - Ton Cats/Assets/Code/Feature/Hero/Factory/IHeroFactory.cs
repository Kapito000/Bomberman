using Factory;
using UnityEngine;

namespace Feature.Hero.Factory
{
	public interface IHeroFactory : IFactory
	{
		int CreateHero(Vector2 pos, Quaternion rot, Transform parent);
		int CreateHeroSpawnPoint(Vector2 pos);
	}
}