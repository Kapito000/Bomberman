using BaseStateMachine;

namespace Animation
{
	public abstract class AnimationStateMachine<TKey>
		: StateMachine<TKey, IAnimationState>
		where TKey : struct
	{ }
}