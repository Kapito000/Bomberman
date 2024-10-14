using Feature.Bomb.Component;
using Infrastructure.ECS;
using UnityEngine;
using Zenject;

namespace Feature.Bomb.Behaviour
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Animator))]
	public sealed class BombAnimator : EntityDependantBehavior
	{
		[Inject] EntityWrapper _bomb;
		
		Animator _animator;
		
		void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		void ExplosionEvent()
		{
			if (TryGetEntity(out var e) ==false)
				return;

			_bomb.SetEntity(e);
			_bomb.Add<Explosion>();
		}
	}
}