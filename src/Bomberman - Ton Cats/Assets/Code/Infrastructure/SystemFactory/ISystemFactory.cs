using Leopotam.EcsLite;

namespace Infrastructure.SystemFactory
{
	public interface ISystemFactory : IService
	{
		TSystem Create<TSystem>() where TSystem : class, IEcsSystem;

		TSystem Create<TSystem>(params object[] args)
			where TSystem : class, IEcsSystem;
	}
}