using Extensions;
using UnityEngine;

namespace Infrastructure.ECS
{
	public partial class EntityWrapper
	{
		public Transform Transform() => 
			_world.Transform(_entity);
	}
}