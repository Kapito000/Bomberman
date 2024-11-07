using UnityEngine;

namespace Feature.Hero.Behaviour
{
	[RequireComponent(typeof(Animator))]
	public sealed class HeroAnimator : MonoBehaviour
	{
		static readonly int _moveUp = Animator.StringToHash("Move up");
		static readonly int _moveDown = Animator.StringToHash("Move down");
		static readonly int _moveLeft = Animator.StringToHash("Move left");
		static readonly int _moveRight = Animator.StringToHash("Move right");
		static readonly int _idle = Animator.StringToHash("Idle");
		static readonly int _death = Animator.StringToHash("Death");

		Animator _animator;

		void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		public void Stop() =>
			_animator.SetTrigger(_idle);

		public void SetMoveDirection(Vector2 direction)
		{
			var x = Mathf.Abs(direction.x);
			var y = Mathf.Abs(direction.y);

			if (x > y)
				_animator.SetTrigger(direction.x > 0 ? _moveRight : _moveLeft);
			else
				_animator.SetTrigger(direction.y > 0 ? _moveUp : _moveDown);
		}

		public void Death() =>
			_animator.SetTrigger(_death);
	}
}