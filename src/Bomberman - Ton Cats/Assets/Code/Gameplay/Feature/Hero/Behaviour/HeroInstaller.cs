using Gameplay.Animation;
using Gameplay.Feature.Hero.Animations;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Hero.Behaviour
{
	public sealed class HeroInstaller : MonoInstaller
	{
		[SerializeField] Animator _animator;

		public override void InstallBindings()
		{
			Container.BindInstance(_animator).AsSingle();
			Container.Bind<BoolAnimationState.Factory>().AsSingle();
			Container.Bind<HeroAnimationStateMachine>().AsSingle();
		}
	}
}