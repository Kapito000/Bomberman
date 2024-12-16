using System;
using System.Collections.Generic;
using Common.Dictionary;
using UnityEngine;
using Menu = Constant.CreateAssetMenu.Path;

namespace Gameplay.StaticData.LevelData
{
	[CreateAssetMenu(menuName = Menu.c_StaticData + nameof(LevelsData))]
	public sealed class LevelsData : ScriptableObject, ILevelsData
	{
		[SerializeField] LevelData[] _list;

		public IReadOnlyList<LevelData> List => _list;
	}

	[Serializable]
	public struct LevelData
	{
		public StringIntegerDictionary Bonuses;
		public StringIntegerDictionary EnemiesAtDoor;
		public StringIntegerDictionary EnemiesAtStart;
	}
}