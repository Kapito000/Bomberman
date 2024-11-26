using System.Collections.Generic;
using Collisions;
using UnityEngine;
using Zenject;

namespace Physics
{
	public sealed class PhysicsService : IPhysicsService
	{
		readonly Collider2D[] _overlapHits = new Collider2D[128];

		[Inject] readonly ICollisionRegistry _collisionRegistry;

		public IEnumerable<int> OverlapCircle(Vector3 position, float radius)
		{
			int hitCount = Physics2D
				.OverlapCircleNonAlloc(position, radius, _overlapHits);

			for (int i = 0; i < hitCount; i++)
			{
				var hasCollider = _collisionRegistry
					.TryGet(_overlapHits[i].GetInstanceID(), out var entity);
				if (hasCollider == false)
					continue;

				yield return entity;
			}
		}
	}
}