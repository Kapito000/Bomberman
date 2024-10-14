using UnityEngine;
using Menu = Constant.CreateAssetMenu;

namespace StaticData.Physic
{
	[CreateAssetMenu(menuName = Menu.Path.c_StaticData + nameof(PhysicsData))]
	public sealed class PhysicsData : ScriptableObject, IPhysicsData
	{
		[field: SerializeField]
		public float OverlapCircleRadius { get; private set; } = .5f;
	}
}