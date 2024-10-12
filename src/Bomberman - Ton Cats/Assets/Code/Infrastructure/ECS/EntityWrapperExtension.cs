using Common;
using Extensions;
using Feature.Camera;
using UnityEngine;
using Rigidbody2D = UnityEngine.Rigidbody2D;
using Transform = UnityEngine.Transform;

namespace Infrastructure.ECS
{
	public partial class EntityWrapper
	{
		public Transform Transform() =>
			_world.Transform(_entity);

		public EntityWrapper AddTransform(Transform transform)
		{
			_world.AddTransform(_entity, transform);
			return this;
		}

		public void SetRigidbody2DVelocity(Vector2 velocity)
		{
			var rb = Rigidbody2D();
			rb.velocity = velocity;
		}

		public Rigidbody2D Rigidbody2D()
		{
			ref var rigidbody2D = ref _world
				.GetPool<Common.Rigidbody2D>()
				.Get(_entity);

			return rigidbody2D.Value;
		}

		public Vector2 MoveDirection()
		{
			ref var moveDirection =
				ref _world.GetPool<MovementDirection>().Get(_entity);
			return moveDirection.Value;
		}

		public float MoveSpeed()
		{
			ref var speed = ref _world.GetPool<MoveSpeed>().Get(_entity);
			return speed.Value;
		}

		public void SetMoveSpeed(float value)
		{
			ref var speed = ref _world.GetPool<MoveSpeed>().Get(_entity);
			speed.Value = value;
		}

		public void AddVirtualCameraFollowTarget(Transform followTarget) =>
			_world.AddFollowTarget(_entity, followTarget);
	}
}