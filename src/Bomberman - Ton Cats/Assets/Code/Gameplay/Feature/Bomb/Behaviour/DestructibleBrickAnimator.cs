using UnityEngine;

namespace Gameplay.Feature.Bomb.Behaviour
{
	public sealed class DestructibleBrickAnimator : MonoBehaviour
	{
		void EndEvent()
		{
			Destroy(gameObject);
		}
	}
}