using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Collisions.Behaviour
{
	public sealed class TriggerExitProcessor : ColliderCheckBehaviour
	{
		[Inject] EntityWrapper _explosion;

		void OnTriggerExit2D(Collider2D other)
		{
			if (!_collisionRegistry.TryGet(other.GetInstanceID(), out var otherEntity))
				return;

			if (TryGetEntity(out var thisEntity) == false)
				return;

			_explosion.SetEntity(thisEntity);
			_explosion.AddToTriggerExitBuffer(otherEntity);
		}
	}
}