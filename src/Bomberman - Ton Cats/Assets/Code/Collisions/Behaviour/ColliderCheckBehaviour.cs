using Infrastructure.ECS;
using Zenject;

namespace Collisions.Behaviour
{
	public abstract class ColliderCheckBehaviour : EntityDependantBehavior
	{
		[Inject] protected ICollisionRegistry _collisionRegistry;
	}
}