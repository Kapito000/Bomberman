using System.Collections.Generic;
using Leopotam.EcsLite;
using LevelData;
using Zenject;
using int_instanceId = System.Int32;

namespace Collisions
{
	public class CollisionRegistry : ICollisionRegistry
	{
		[Inject] ILevelData _levelData;

		readonly Dictionary<int_instanceId, EcsPackedEntity> _entityByInstanceId = new();

		public void Register(int instanceId, int entity)
		{
			_entityByInstanceId[instanceId] = World().PackEntity(entity);
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

			if (pack.Unpack(World(), out entity) == false)
				return false;

			return true;
		}

		EcsWorld World() => 
			_levelData.World;
	}
}