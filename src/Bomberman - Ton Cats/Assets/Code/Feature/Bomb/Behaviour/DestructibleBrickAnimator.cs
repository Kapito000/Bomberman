using UnityEngine;

namespace Feature.Bomb.Behaviour
{
	public sealed class DestructibleBrickAnimator : MonoBehaviour
	{
		void EndEvent()
		{
			Destroy(gameObject);
		}
	}
}