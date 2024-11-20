using System.Collections.Generic;
using Leopotam.EcsLite;
using Zenject;
using int_instanceId = System.Int32;

namespace Collisions
{
	public class CollisionRegistry : ICollisionRegistry
	{
		[Inject] EcsWorld _world;

		readonly Dictionary<int_instanceId, EcsPackedEntity> _entityByInstanceId = new();

		public void Register(int instanceId, int entity)
		{
			_entityByInstanceId[instanceId] = _world.PackEntity(entity);
		}

		public void Unregister(int instanceId)
		{
			if (_entityByInstanceId.ContainsKey(instanceId))
				_entityByInstanceId.Remove(instanceId);
		}

		public bool TryGet(int instanceId, out int entity)
		{
			entity = default;
			
			if (_entityByInstanceId.TryGetValue(instanceId, out var pack) == false)
				return false;

			if (pack.Unpack(_world, out entity) == false)
				return false;

			return true;
		}
	}
}