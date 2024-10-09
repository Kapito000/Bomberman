using Leopotam.EcsLite;
using Zenject;

namespace Infrastructure.SystemFactory
{
	public sealed class StandardSystemFactory : ISystemFactory
	{
		[Inject] EcsWorld _world;
		[Inject] DiContainer _container;

		public TSystem Create<TSystem>() where TSystem : class, IEcsSystem
		{
			return _container.Instantiate<TSystem>();
		}

		public TSystem Create<TSystem>(params object[] args)
			where TSystem : class, IEcsSystem
		{
			return _container.Instantiate<TSystem>(args);
		}
	}
}