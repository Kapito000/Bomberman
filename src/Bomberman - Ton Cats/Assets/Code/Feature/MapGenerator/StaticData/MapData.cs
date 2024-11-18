using UnityEngine;
using Menu = Constant.CreateAssetMenu;

namespace Feature.MapGenerator.StaticData
{
	[CreateAssetMenu(menuName = Menu.Path.c_StaticData + nameof(MapData))]
	public sealed class MapData : ScriptableObject, IMapData
	{
		[field: SerializeField] public Vector2Int MapSize { get; private set; }
	}
}