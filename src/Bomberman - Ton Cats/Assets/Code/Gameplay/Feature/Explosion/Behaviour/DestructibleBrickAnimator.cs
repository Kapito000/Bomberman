using UnityEngine;

namespace Gameplay.Feature.Explosion.Behaviour
{
	public sealed class DestructibleBrickAnimator : MonoBehaviour
	{
		void EndEvent()
		{
			Destroy(gameObject);
		}
	}
}