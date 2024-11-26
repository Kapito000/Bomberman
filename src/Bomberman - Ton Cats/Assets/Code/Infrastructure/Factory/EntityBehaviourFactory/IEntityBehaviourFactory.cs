using UnityEngine;

namespace Infrastructure.Factory.EntityBehaviourFactory
{
	public interface IEntityBehaviourFactory : IFactory
	{
		int InitEntityBehaviour(GameObject obj);
	}
}