using UnityEngine;

namespace Infrastructure.ECS
{
	public abstract class EntityDependantBehavior : MonoBehaviour
	{
		EntityBehaviour _entityView;

		public void Init(EntityBehaviour entityView) =>
			_entityView = entityView;

		protected bool TryGetEntity(out int entity) =>
			_entityView.TryGetEntity(out entity);
	}
}