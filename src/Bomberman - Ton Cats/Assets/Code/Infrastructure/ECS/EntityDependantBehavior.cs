using UnityEngine;

namespace Infrastructure.ECS
{
	public abstract class EntityDependantBehavior : MonoBehaviour
	{
		IEntityView _entityView;

		public void Init(IEntityView entityView) =>
			_entityView = entityView;

		protected bool TryGetEntity(out int entity) =>
			_entityView.TryGetEntity(out entity);
	}
}