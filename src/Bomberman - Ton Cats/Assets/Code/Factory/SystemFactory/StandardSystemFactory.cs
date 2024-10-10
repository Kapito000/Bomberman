using Leopotam.EcsLite;
using Zenject;

namespace Factory.SystemFactory
{
	public sealed class StandardSystemFactory : ISystemFactory
	{
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

		public EcsSystems CreateSystemGroup() => 
			_container.Instantiate<EcsSystems>();
	}
}