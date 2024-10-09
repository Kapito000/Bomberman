using Infrastructure.SystemFactory;
using Leopotam.EcsLite;

namespace Infrastructure
{
	public abstract class Feature : IEcsSystem
	{
		readonly EcsSystems _systems;
		readonly ISystemFactory _systemFactory;

		protected Feature(EcsSystems systems, ISystemFactory systemFactory)
		{
			_systems = systems;
			_systemFactory = systemFactory;
		}

		protected void Add<TSystem>() where TSystem : class, IEcsSystem
		{
			_systems.Add(_systemFactory.Create<TSystem>());
		}
	}
}