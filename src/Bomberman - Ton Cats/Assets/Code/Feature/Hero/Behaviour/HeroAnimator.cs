using Feature.Hero.Animations;
using UnityEngine;
using Zenject;

namespace Feature.Hero.Behaviour
{
	[RequireComponent(typeof(Animator))]
	public sealed class HeroAnimator : MonoBehaviour
	{
		// [Inject] Animator _animator;
		[Inject] HeroAnimationStateMachine _stateMachine;

		void Awake()
		{
			_stateMachine.Init(State.Idle);
		}

		public void Stop() =>
			_stateMachine.Enter(State.Idle);

		public void SetMoveDirection(Vector2 direction)
		{
			var x = Mathf.Abs(direction.x);
			var y = Mathf.Abs(direction.y);

			if (x > y)
				_stateMachine.Enter(direction.x > 0 ? State.MoveRight : State.MoveLeft);
			else
				_stateMachine.Enter(direction.y > 0 ? State.MoveUp : State.MoveDown);
		}

		public void Death() =>
			_stateMachine.Enter(State.Death);
	}
}