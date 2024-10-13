using UnityEngine;

namespace Infrastructure.ECS
{
	public interface IEntityView
	{
		GameObject gameObject { get; }
		void SetEntity(int e);
		bool TryGetEntity(out int entity);
	}
}