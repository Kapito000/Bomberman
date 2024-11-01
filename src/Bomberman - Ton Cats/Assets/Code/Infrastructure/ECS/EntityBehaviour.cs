using AB_Utility.FromSceneToEntityConverter;
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

		public void AddToEcs(out int entity)
		{
			entity = _world.NewEntity();
			SetEntity(entity);
			ConvertConverters(_world, entity);
			ResolveEntityDependant();
		}

		public bool TryGetEntity(out int entity)
		{
			if (_packedEntity.Unpack(_world, out entity) == false)
				return false;

			return true;
		}

		void SetEntity(int e)
		{
			_packedEntity = _world.PackEntity(e);
			_world.AddView(e, this);
		}

		void ConvertConverters(EcsWorld world, int entity)
		{
			if (false == TryGetComponent<ComponentsContainer>(out var container))
				return;

			var destroyAfterConversion = container.DestroyAfterConversion;
			var packedEntity = world.PackEntityWithWorld(entity);

			for (int j = 0; j < container.Converters.Length; j++)
			{
				var converter = container.Converters[j];
				converter.Convert(packedEntity);

				if (destroyAfterConversion)
					Destroy(converter);
			}

			if (destroyAfterConversion)
				Destroy(container);
		}

		void ResolveEntityDependant()
		{
			var dependants = GetComponents<EntityDependantBehavior>();
			foreach (var dependant in dependants)
			{
				dependant.Init(this);
			}
		}
	}
}