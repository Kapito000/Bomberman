using Common.Component;
using Extensions;
using Feature.Bomb.Component;
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

		public Vector3 TransformPos() =>
			_world.TransformPos(_entity);

		public EntityWrapper SetTransform(Transform transform)
		{
			_world.SetTransform(_entity, transform);
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
				.GetPool<Common.Component.Rigidbody2D>()
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

		public void SetBombNumber(int value)
		{
			ref var bombNumber = ref _world.GetPool<BombNumber>().Get(_entity);
			bombNumber.Value = value;
		}

		public int BombNumber()
		{
			ref var bombNumber = ref Get<BombNumber>();
			return bombNumber.Value;
		}

		public IEntityView View()
		{
			ref var view = ref Get<View>();
			return view.Value;
		}

		public Vector3 Position()
		{
			ref var position = ref _world.GetPool<Position>().Get(_entity);
			return position.Value;
		}

		public void SetPosition(Vector2 pos)
		{
			ref var position = ref Get<Position>();
			position.Value = pos;
		}
	}
}