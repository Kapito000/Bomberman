using UnityEngine;
using Menu = Constant.CreateAssetMenu.Path;

namespace Gameplay.Feature.Bomb.StaticData
{
	[CreateAssetMenu(menuName = Menu.c_StaticData + nameof(BombDataService))]
	public sealed class BombDataService : ScriptableObject, IBombDataService
	{
		[SerializeField] TextAsset _tsv;
	}
}