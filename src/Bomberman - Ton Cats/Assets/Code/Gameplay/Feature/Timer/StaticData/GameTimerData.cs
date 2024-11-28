using Gameplay.Feature.Timer.Component;
using UnityEngine;
using Menu = Constant.CreateAssetMenu;

namespace Gameplay.Feature.Timer.StaticData
{
	[CreateAssetMenu(menuName = Menu.Path.c_StaticData + nameof(GameTimer))]
	public sealed class GameTimerData : ScriptableObject, IGameTimerData
	{
		[field: SerializeField] public float Value { get; private set; } = 180f;
	}
}