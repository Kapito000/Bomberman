using System.Collections.Generic;

namespace Gameplay.StaticData.LevelData
{
	public interface ILevelsData : IStaticData
	{
		IReadOnlyDictionary<string, float>[] Bonuses { get; }
		IReadOnlyDictionary<string, float>[] EnemiesAtDoor { get; }
		IReadOnlyDictionary<string, float>[] EnemiesAtStart { get; }
		void Init();
	}
}