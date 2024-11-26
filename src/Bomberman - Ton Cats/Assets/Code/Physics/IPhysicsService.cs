using System.Collections.Generic;
using Infrastructure;
using UnityEngine;

namespace Physics
{
	public interface IPhysicsService : IService
	{
		IEnumerable<int> OverlapCircle(Vector3 position, float radius);
	}
}