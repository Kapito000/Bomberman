using System.Collections.Generic;
using Gameplay.Collisions;
using UnityEngine;
using Zenject;

namespace Gameplay.Physics
{
	public sealed class PhysicsService : IPhysicsService
	{
		readonly Collider2D[] _overlapHits = new Collider2D[128];

		[Inject] readonly ICollisionRegistry _collisionRegistry;

		public IEnumerable<int> OverlapPoint(Vector2 worldPosition)
		{
			int hitCount = Physics2D
				.OverlapPointNonAlloc(worldPosition, _overlapHits);

			for (int i = 0; i < hitCount; i++)
			{
				Collider2D hit = _overlapHits[i];
				if (hit == null)
					continue;

				if (false == TryGetEntityFromRegistry(i, out var entity))
					continue;

				yield return entity;
			}
		}

		public IEnumerable<int> OverlapCircle(Vector3 position, float radius)
		{
			int hitCount = Physics2D
				.OverlapCircleNonAlloc(position, radius, _overlapHits);

			for (int i = 0; i < hitCount; i++)
			{
				if (false == TryGetEntityFromRegistry(i, out var entity))
					continue;

				yield return entity;
			}
		}

		bool TryGetEntityFromRegistry(int index, out int entity)
		{
			var instanceID = _overlapHits[index].GetInstanceID();
			return _collisionRegistry.TryGet(instanceID, out entity);
		}
	}
}