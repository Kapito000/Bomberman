using UnityEngine;
using Menu = Constant.CreateAssetMenu.Path;

namespace Gameplay.Feature.Enemy.Base.StaticData
{
	[CreateAssetMenu(menuName = Menu.c_StaticData + nameof(EnemyList))]
	public class EnemyList : ScriptableObject
	{
		public EnemyDataDictionary List;
	}
}