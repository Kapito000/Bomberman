using StaticData;

namespace Feature.Hero.StaticData
{
	public interface IHeroData : IStaticData
	{
		float MovementSpeed { get; }
		int StartBombNumber { get; }
		int LifePointsOnStart { get; }
	}
}