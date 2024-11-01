using System;
using UnityEngine;

namespace Infrastructure.ECS
{
	public interface IEntityView : IDisposable
	{
		GameObject gameObject { get; }
		bool TryGetEntity(out int entity);
	}
}