using System;
using Common;

namespace Gameplay.Feature.Enemy.Base.StaticData
{
	[Serializable]
	public sealed class EnemyDataDictionary : SerializedDictionary<string, EnemyData>
	{ }
}