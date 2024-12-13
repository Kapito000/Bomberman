﻿using System;
using Common;
using Common.Dictionary;

namespace Gameplay.Feature.Enemy.Base.StaticData
{
	[Serializable]
	public sealed class EnemyDataDictionary : SerializedDictionary<string, EnemyData>
	{ }
}