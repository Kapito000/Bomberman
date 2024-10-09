using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Feature.Hero.System
{
	public sealed class HeroSystem : IEcsRunSystem, IEcsInitSystem
	{
		EcsFilterInject<Inc<Component.Hero>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _filter.Value)
			{ }
		}

		public void Init(IEcsSystems systems)
		{
			var e = systems.GetWorld().NewEntity();
			systems.GetWorld().GetPool<Component.Hero>().Add(e);
		}
	}
}