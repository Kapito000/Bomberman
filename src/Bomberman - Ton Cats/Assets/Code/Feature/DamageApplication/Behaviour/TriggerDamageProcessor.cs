using Collisions.Behaviour;
using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Feature.DamageApplication.Behaviour
{
	public sealed class TriggerDamageProcessor : ColliderCheckBehaviour
	{
		[Inject] EntityWrapper _explosion;

		void OnTriggerEnter2D(Collider2D other)
		{
			if (!_collisionRegistry.TryGet(other.GetInstanceID(), out var otherEntity))
				return;

			if (TryGetEntity(out var thisEntity) == false)
				return;

			_explosion.SetEntity(thisEntity);
			_explosion.AddToDamageBufferRequest(otherEntity);
		}

		void OnTriggerExit2D(Collider2D other)
		{
			if (!_collisionRegistry.TryGet(other.GetInstanceID(), out var otherEntity))
				return;

			if (TryGetEntity(out var thisEntity) == false)
				return;

			_explosion.SetEntity(thisEntity);
			_explosion.RemoveFromDamageBufferRequest(otherEntity);
		}
	}
}