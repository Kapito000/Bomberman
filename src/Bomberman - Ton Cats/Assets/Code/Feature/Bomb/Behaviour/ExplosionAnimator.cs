using Feature.Destruction.Component;
using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Feature.Bomb.Behaviour
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Animator))]
	public sealed class ExplosionAnimator : EntityDependantBehavior
	{
		static readonly int _centerHash = Animator.StringToHash("Center");
		static readonly int _middleHash = Animator.StringToHash("Middle");
		static readonly int _endHash = Animator.StringToHash("End");

		[Inject] EntityWrapper _explosion;
		
		Animator _animator;

		void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		public void PlayCenter() => _animator.SetTrigger(_centerHash);
		public void PlayMiddle() => _animator.SetTrigger(_middleHash);
		public void PlayEnd() => _animator.SetTrigger(_endHash);

		void EndEvent()
		{
			if (TryGetEntity(out var e) == false)
				return;

			_explosion.SetEntity(e);
			_explosion.Add<Destructed>();
		}
	}
}