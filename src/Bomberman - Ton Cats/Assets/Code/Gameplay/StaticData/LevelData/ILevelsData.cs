using System.Collections.Generic;

namespace Gameplay.StaticData.LevelData
{
	public interface ILevelsData : IStaticData
	{
		IReadOnlyList<LevelData> List { get; }
	}
}