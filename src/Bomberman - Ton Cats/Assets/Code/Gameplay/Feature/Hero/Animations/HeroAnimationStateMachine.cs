using System.Collections.Generic;
using Gameplay.Animation;
using Gameplay.Feature.Hero.Behaviour;

namespace Gameplay.Feature.Hero.Animations
{
	public sealed class HeroAnimationStateMachine : AnimationStateMachine<State>
	{
		public HeroAnimationStateMachine(BoolAnimationState.Factory factory)
		{
			_states = new Dictionary<State, IAnimationState>()
			{
				{ State.Idle, factory.Create(Hash.Idle) },
				{ State.Death, factory.Create(Hash.Death) },
				{ State.MoveUp, factory.Create(Hash.MoveUp) },
				{ State.MoveDown, factory.Create(Hash.MoveDown) },
				{ State.MoveLeft, factory.Create(Hash.MoveLeft) },
				{ State.MoveRight, factory.Create(Hash.MoveRight) },
			};
		}

		protected override bool IsEquals(State x, State y) => x == y;
	}
}