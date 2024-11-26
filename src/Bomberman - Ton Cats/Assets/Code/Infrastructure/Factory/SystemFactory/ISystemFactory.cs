using Leopotam.EcsLite;

namespace Infrastructure.Factory.SystemFactory
{
	public interface ISystemFactory : IFactory
	{
		TSystem Create<TSystem>() where TSystem : class, IEcsSystem;

		TSystem Create<TSystem>(params object[] args)
			where TSystem : class, IEcsSystem;

		EcsSystems CreateSystemGroup();
	}
}