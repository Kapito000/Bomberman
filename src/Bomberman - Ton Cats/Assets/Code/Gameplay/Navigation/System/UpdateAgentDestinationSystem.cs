using Common.Component;
using Gameplay.Feature.Enemy;
using Gameplay.Navigation.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Navigation.System
{
	public sealed class UpdateAgentDestinationSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _agent;
		
		readonly EcsFilterInject<Inc<NavMeshAgentComponent, AgentDestination>>
			_agentFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var agentEntity in _agentFilter.Value)
			{
				_agent.SetEntity(agentEntity);
				
				var navMeshAgent = _agent.NavMeshAgent();
				var agentDestination = _agent.AgentDestination();
				navMeshAgent.destination = agentDestination;
				
				_agent.Remove<AgentDestination>();
			}
		}
	}
}