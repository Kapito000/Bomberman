using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Collisions.Behaviour
{
	public sealed class TriggerEnterProcessor : ColliderCheckBehaviour
	{
		[Inject] EntityWrapper _explosion;

		void OnTriggerEnter2D(Collider2D other)
		{
			if (!_collisionRegistry.TryGet(other.GetInstanceID(), out var otherEntity))
				return;

			if (TryGetEntity(out var thisEntity) == false)
				return;

			_explosion.SetEntity(thisEntity);
			_explosion.AddToTriggerEnterBuffer(otherEntity);
		}
	}
}