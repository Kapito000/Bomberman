using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Collisions.Behaviour
{
	public sealed class TriggerEnterProcessor : ColliderCheckBehaviour
	{
		[Inject] EntityWrapper _thisWrapper;

		void OnTriggerEnter2D(Collider2D other)
		{
			if (!_collisionRegistry.TryGet(other.GetInstanceID(), out var otherEntity))
				return;

			if (TryGetEntity(out var thisEntity) == false)
				return;

			_thisWrapper.SetEntity(thisEntity);
			_thisWrapper.AddToTriggerEnterBuffer(otherEntity);
		}
	}
}