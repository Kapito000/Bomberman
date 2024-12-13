using System.Collections.Generic;
using Gameplay.StaticData;

namespace Gameplay.Feature.Enemy.Base.StaticData
{
	public interface IEnemyList : IStaticData
	{
		IReadOnlyDictionary<string, EnemyData> Dictionary { get; }
		bool TryGet(string key, out EnemyData data);
	}
}