using Extensions;
using Gameplay.Feature.Destruction.Component;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS
{
	public partial class EntityWrapper
	{
		int _entity;
		readonly EcsWorld _world;
		
		public EntityWrapper(EcsWorld world)
		{
			_world = world;
		}

		public void SetEntity(int entity)
		{
			_entity = entity;
		}

		public EntityWrapper NewEntity()
		{
			SetEntity(_world.NewEntity());
			return this;
		}

		public ref TComponent AddComponent<TComponent>()
			where TComponent : struct
		{
			ref var component = ref _world.AddComponent<TComponent>(_entity);
			return ref component;
		}

		public EntityWrapper Add<TComponent>()
			where TComponent : struct
		{
			AddComponent<TComponent>();
			return this;
		}

		public EntityWrapper Remove<TComponent>()
			where TComponent : struct
		{
			_world.Remove<TComponent>(_entity);
			return this;
		}

		public ref TComponent Get<TComponent>()
			where TComponent : struct =>
			ref _world.Get<TComponent>(_entity);

		public ref TComponent ReplaceComponent<TComponent>()
			where TComponent : struct
		{
			ref var component = ref _world.Replace<TComponent>(_entity);
			return ref component;
		}

		public EntityWrapper Replace<TComponent>()
			where TComponent : struct
		{
			ReplaceComponent<TComponent>();
			return this;
		}

		public bool Has<TComponent>() where TComponent : struct
		{
			return _world.Has<TComponent>(_entity);
		}

		public void Destroy()
		{
			Add<Destructed>();
		}
	}
}