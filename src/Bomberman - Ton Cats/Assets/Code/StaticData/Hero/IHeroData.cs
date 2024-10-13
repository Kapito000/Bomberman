namespace StaticData.Hero
{
	public interface IHeroData : IStaticData
	{
		float MovementSpeed { get; }
		int StartBombNumber { get; }
	}
}