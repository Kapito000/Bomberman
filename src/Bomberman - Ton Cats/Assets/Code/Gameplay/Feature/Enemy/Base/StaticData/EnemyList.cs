using System.Collections.Generic;
using UnityEngine;
using Menu = Constant.CreateAssetMenu.Path;

namespace Gameplay.Feature.Enemy.Base.StaticData
{
	[CreateAssetMenu(menuName = Menu.c_StaticData + nameof(EnemyList))]
	public sealed class EnemyList : ScriptableObject, IEnemyList
	{
		[SerializeField] EnemyDataDictionary _dictionary;

		public IReadOnlyDictionary<string, EnemyData> Dictionary => _dictionary;

		public bool TryGet(string key, out EnemyData data) =>
			_dictionary.TryGetValue(key, out data);
	}
}