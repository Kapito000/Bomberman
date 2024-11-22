using AI.Navigation;
using Feature.Map.Component;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.Map.System
{
	public sealed class RebakeNavigationSurfaceSystem : IEcsRunSystem
	{
		[Inject] INavigationSurface _navigationSurface;

		readonly EcsFilterInject<Inc<DestroyedTile>> _destroyedTileFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _destroyedTileFilter.Value)
				_navigationSurface.Update();
		}
	}
}