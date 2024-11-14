using UnityEngine;
using UnityEngine.AI;

namespace Common.Behaviour
{
	[RequireComponent(typeof(NavMeshAgent))]
	public sealed class NavMeshAgentRotationLock : MonoBehaviour
	{
		void Awake()
		{
			var agent = GetComponent<NavMeshAgent>();
			agent.updateRotation = false;
			agent.updateUpAxis = false;
		}
	}
}