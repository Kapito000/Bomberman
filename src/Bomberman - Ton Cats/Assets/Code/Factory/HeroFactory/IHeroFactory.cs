using UnityEngine;

namespace Factory.HeroFactory
{
	public interface IHeroFactory : IFactory
	{
		GameObject CreateHero(Vector2 pos, Quaternion rot, Transform parent);
	}
}