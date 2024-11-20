using Collisions.Behaviour;
using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Feature.Explosion.Behaviour
{
	public sealed class ExplosionTriggerProcessor : ColliderCheckBehaviour
	{
		[Inject] EntityWrapper _explosion;

		void OnTriggerEnter2D(Collider2D other)
		{
			if (!_collisionRegistry.TryGet(other.GetInstanceID(), out var otherEntity))
				return;

			if (TryGetEntity(out var thisEntity) == false)
				return;

			_explosion.SetEntity(thisEntity);
			_explosion.TryAddToTargetBuffer(otherEntity);
		}

		void OnTriggerExit2D(Collider2D other)
		{
			if (!_collisionRegistry.TryGet(other.GetInstanceID(), out var otherEntity))
				return;

			if (TryGetEntity(out var thisEntity) == false)
				return;

			_explosion.SetEntity(thisEntity);
			_explosion.TryRemoveFromTargetBuffer(otherEntity);
		}
	}
}