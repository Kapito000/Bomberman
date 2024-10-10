using UnityEngine;

namespace Factory.HeroFactory
{
	public interface IHeroFactory : IFactory
	{
		int CreateHero(Vector2 pos, Quaternion rot, Transform parent);
	}
}