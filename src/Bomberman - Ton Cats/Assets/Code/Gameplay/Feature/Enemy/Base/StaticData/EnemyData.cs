using UnityEngine;
using Menu = Constant.CreateAssetMenu.Path;

namespace Gameplay.Feature.Enemy.Base.StaticData
{
	[CreateAssetMenu(menuName = Menu.c_StaticData + nameof(EnemyData))]
	public class EnemyData : ScriptableObject
	{
		public string Name;
		public EnemyCharacteristic Characteristic;
	}
}