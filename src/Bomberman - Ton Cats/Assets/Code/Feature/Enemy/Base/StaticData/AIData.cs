using UnityEngine;
using Menu  = Constant.CreateAssetMenu;

namespace Feature.Enemy.Base.StaticData
{
	[CreateAssetMenu(menuName = Menu.Path.c_StaticData + nameof(AIData))]
	public sealed class AIData : ScriptableObject, IAIData
	{
		[field: SerializeField] public int PatrolDistance { get; private set; } = 5;
		[field: SerializeField] public float ArrivedDestinationDistance { get; private set; } = .5f;
	}
}