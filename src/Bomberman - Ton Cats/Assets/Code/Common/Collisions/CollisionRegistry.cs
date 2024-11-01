using System.Collections.Generic;
using int_instanceId = System.Int32;
using int_entity = System.Int32;

namespace Common.Collisions
{
	public class CollisionRegistry : ICollisionRegistry
	{
		readonly Dictionary<int_instanceId, int_entity> _entityByInstanceId = new();

		public void Register(int instanceId, int entity)
		{
			_entityByInstanceId[instanceId] = entity;
		}

		public void Unregister(int instanceId)
		{
			if (_entityByInstanceId.ContainsKey(instanceId))
				_entityByInstanceId.Remove(instanceId);
		}

		public bool TryGet(int instanceId, out int entity) =>
			_entityByInstanceId.TryGetValue(instanceId, out entity);
	}
}