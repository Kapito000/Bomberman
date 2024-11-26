using Gameplay.AI.Navigation;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.MapGenerator.System
{
	public sealed class BindNavMeshSystem : IEcsRunSystem
	{
		[Inject] INavigationSurface _navigationSurface;

		public void Run(IEcsSystems systems)
		{
			_navigationSurface.Bake();
		}
	}
}