using StaticData;

namespace Feature.Enemy.Base.StaticData
{
	public interface IAIData : IStaticData
	{
		float ArrivedDestinationDistance { get; }
		int PatrolDistance { get; }
	}
}