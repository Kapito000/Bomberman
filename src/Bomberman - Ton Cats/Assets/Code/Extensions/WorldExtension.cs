using Cinemachine;
using Common.Component;
using Feature.Camera.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using UnityEngine;
using Transform = Common.Component.Transform;

namespace Extensions
{
	public static class WorldExtension
	{
		public static void AddToEcs(this EcsWorld world, EntityBehaviour behaviour,
			out int entity)
		{
			entity = world.NewEntity();
			behaviour.SetEntity(entity);
			behaviour.ConvertConverters(world, entity);
			behaviour.ResolveEntityDependant();
		}

		public static void AddView(this EcsWorld world, int e,
			IEntityView entityView)
		{
			ref var view = ref world.AddComponent<View>(e);
			view.Value = entityView;
		}

		public static void DestroyImmediate(this EcsWorld world, int e)
		{
			var viewPool = world.GetPool<View>();
			if (viewPool.Has(e))
			{
				ref var view = ref world.Get<View>(e);
				Object.Destroy(view.Value.gameObject);
			}
			world.DelEntity(e);
		}

		public static UnityEngine.Transform Transform(this EcsWorld world, int e)
		{
			ref var transform = ref world.GetPool<Transform>().Get(e);
			return transform.Value;
		}

		public static Vector3 TransformPos(this EcsWorld world, int e)
		{
			ref var transform = ref world.GetPool<Transform>().Get(e);
			return transform.Value.position;
		}

		public static void SetTransform(this EcsWorld world, int e,
			UnityEngine.Transform transform)
		{
			ref var transformComponent = ref world.GetPool<Transform>().Get(e);
			transformComponent.Value = transform;
		}

		public static void SetMovementDirection(this EcsWorld world, int e,
			Vector2 value)
		{
			ref var movableDirection = ref world.GetPool<MovementDirection>().Get(e);
			movableDirection.Value = value;
		}

		public static CinemachineVirtualCamera VirtualCamera(this EcsWorld world,
			int e)
		{
			ref var virtualCamera = ref world.GetPool<VirtualCamera>().Get(e);
			return virtualCamera.Value;
		}

		public static UnityEngine.Transform FollowTarget(this EcsWorld world, int e)
		{
			ref var setFollowTarget = ref world.GetPool<FollowTarget>().Get(e);
			return setFollowTarget.Value;
		}

		public static ref TComponent AddComponent<TComponent>(this EcsWorld world,
			int e)
			where TComponent : struct =>
			ref world.GetPool<TComponent>().Add(e);

		public static ref TComponent Replace<TComponent>(this EcsWorld world, int e)
			where TComponent : struct
		{
			var pool = world.GetPool<TComponent>();
			if (pool.Has(e))
				return ref pool.Get(e);

			return ref pool.Add(e);
		}

		public static void Remove<TComponent>(this EcsWorld world, int e)
			where TComponent : struct =>
			world.GetPool<TComponent>().Del(e);

		public static ref TComponent Get<TComponent>(this EcsWorld world, int e)
			where TComponent : struct =>
			ref world.GetPool<TComponent>().Get(e);

		public static bool Has<TComponent>(this EcsWorld world, int e)
			where TComponent : struct =>
			world.GetPool<TComponent>().Has(e);

		public static bool Has(this IEcsPool pool, int e) =>
			pool.Has(e);
	}
}