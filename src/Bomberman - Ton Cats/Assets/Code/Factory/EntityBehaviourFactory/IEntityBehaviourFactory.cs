using Factory.Kit;
using UnityEngine;

namespace Factory.EntityBehaviourFactory
{
	public interface IEntityBehaviourFactory : IFactory
	{
		int CreateEntityBehaviour(GameObject obj);
	}
}