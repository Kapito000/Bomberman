using Gameplay.StaticData;

namespace Gameplay.Feature.Timer.StaticData
{
	public interface IGameTimerData : IStaticData
	{
		float Value { get; }
	}
}