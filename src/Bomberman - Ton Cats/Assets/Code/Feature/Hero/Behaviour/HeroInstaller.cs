using Animation;
using Feature.Hero.Animations;
using UnityEngine;
using Zenject;

namespace Feature.Hero.Behaviour
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