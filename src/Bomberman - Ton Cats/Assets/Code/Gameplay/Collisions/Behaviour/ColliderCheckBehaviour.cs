using Infrastructure.ECS;
using Zenject;

namespace Gameplay.Collisions.Behaviour
{
	public abstract class ColliderCheckBehaviour : EntityDependantBehavior
	{
		[Inject] protected ICollisionRegistry _collisionRegistry;
	}
}