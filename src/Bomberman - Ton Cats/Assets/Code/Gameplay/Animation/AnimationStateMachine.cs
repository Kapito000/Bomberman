using Common.BaseStateMachine;

namespace Gameplay.Animation
{
	public abstract class AnimationStateMachine<TKey>
		: StateMachine<TKey, IAnimationState>
		where TKey : struct
	{ }
}