using Extensions;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Infrastructure.ECS
{
	public class EntityBehaviour : MonoBehaviour, IEntityView
	{
		[Inject] EcsWorld _world;

		EcsPackedEntity _packedEntity;

		public void SetEntity(int e)
		{
			_packedEntity = _world.PackEntity(e);
			_world.AddView(e, this);
		}

		public bool TryGetEntity(out int entity)
		{
			if (_packedEntity.Unpack(_world, out entity) == false)
				return false;

			return true;
		}
	}
}