namespace StaticData.Physic
{
	public interface IPhysicsData : IStaticData
	{
		float OverlapCircleRadius { get; }
	}
}