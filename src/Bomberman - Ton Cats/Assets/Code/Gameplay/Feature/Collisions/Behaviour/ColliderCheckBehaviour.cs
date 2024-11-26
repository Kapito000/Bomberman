using Gameplay.Collisions;
using Infrastructure.ECS;
using Zenject;

namespace Gameplay.Feature.Collisions.Behaviour
{
	public abstract class ColliderCheckBehaviour : EntityDependantBehavior
	{
		[Inject] protected ICollisionRegistry _collisionRegistry;
	}
}