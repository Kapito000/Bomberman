using UnityEngine;

namespace Feature.Explosion.Behaviour
{
	public sealed class DestructibleBrickAnimator : MonoBehaviour
	{
		void EndEvent()
		{
			Destroy(gameObject);
		}
	}
}