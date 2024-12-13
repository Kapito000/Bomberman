using System;
using Common.Dictionary;
using UnityEngine;
using Menu = Constant.CreateAssetMenu.Path;

namespace Gameplay.StaticData.LevelData
{
	[CreateAssetMenu(menuName = Menu.c_StaticData + nameof(LevelDataList))]
	public sealed class LevelDataList : ScriptableObject, ILevelDataList
	{
		public LevelData[] List;
	}

	[Serializable]
	public struct LevelData
	{
		public StringIntegerDictionary Bonuses;
		public StringIntegerDictionary EnemiesAtDoor;
		public StringIntegerDictionary EnemiesAtStart;
	}
}