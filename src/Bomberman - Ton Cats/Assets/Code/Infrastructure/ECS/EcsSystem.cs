using Leopotam.EcsLite;
using Zenject;

namespace Infrastructure.ECS
{
	public abstract class EcsSystem
	{
		[Inject] protected EcsWorld _world;
	}
}