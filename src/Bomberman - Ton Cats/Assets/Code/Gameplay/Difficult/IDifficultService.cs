﻿using System.Collections.Generic;
using Infrastructure;

namespace Gameplay.Difficult
{
	public interface IDifficultService : IService
	{
		IReadOnlyDictionary<string, int> EnemyAtStartForCurrentProgress();
	}
}