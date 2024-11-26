using UnityEngine;

namespace Gameplay.Animation
{
	public class BoolAnimationState : IAnimationState
	{
		readonly int _keyHash;
		readonly Animator _animator;

		public BoolAnimationState(int keyHash, Animator animator)
		{
			_keyHash = keyHash;
			_animator = animator;
		}

		public void Enter()
		{
			_animator.SetBool(_keyHash, true);
		}

		public void Exit()
		{
			_animator.SetBool(_keyHash, false);
		}

		public sealed class Factory
		{
			readonly Animator _animator;
			
			public Factory(Animator animator) => 
				_animator = animator;

			public BoolAnimationState Create(int keyHash) => 
				new(keyHash, _animator);
		}
	}
}