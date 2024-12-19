using Common.Dictionary;

namespace Gameplay.StaticData.LevelData
{
	public interface ILevelsData : IStaticData
	{
		StringIntegerDictionary[] Bonuses { get; }
		StringIntegerDictionary[] EnemiesAtDoor { get; }
		StringIntegerDictionary[] EnemiesAtStart { get; }
	}
}