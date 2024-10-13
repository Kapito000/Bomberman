using Extensions;
using Leopotam.EcsLite;

namespace Infrastructure.ECS
{
	public partial class EntityWrapper
	{
		int _entity;
		EcsWorld _world;

		public EntityWrapper(EcsWorld world)
		{
			_world = world;
		}

		public void SetEntity(int entity)
		{
			_entity = entity;
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
	}
}