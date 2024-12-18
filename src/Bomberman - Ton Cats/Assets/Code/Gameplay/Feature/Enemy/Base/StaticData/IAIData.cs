using Gameplay.StaticData;

namespace Gameplay.Feature.Enemy.Base.StaticData
{
	public interface IAIData : IStaticData
	{
		int PatrolDistance { get; }
		float ArrivedDestinationDistance { get; }
		float AfterDoorImmortalTimer { get; }
	}
}