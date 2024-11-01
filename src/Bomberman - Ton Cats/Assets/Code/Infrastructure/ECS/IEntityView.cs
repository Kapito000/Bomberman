using UnityEngine;

namespace Infrastructure.ECS
{
	public interface IEntityView
	{
		GameObject gameObject { get; }
		bool TryGetEntity(out int entity);
	}
}