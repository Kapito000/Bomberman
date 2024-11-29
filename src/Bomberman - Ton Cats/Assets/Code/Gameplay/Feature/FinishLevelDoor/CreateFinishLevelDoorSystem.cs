using Gameplay.Feature.Map.MapController;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.FinishLevelDoor
{
	public sealed class CreateFinishLevelDoorSystem : IEcsRunSystem
	{
		[Inject] IMapController _mapController;
		
		public void Run(IEcsSystems systems)
		{
			// _mapController.
		}
	}
}