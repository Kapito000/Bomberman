using System;
using Factory.SystemFactory;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Infrastructure.ECS
{
	public abstract class Feature : IEcsSystem, IDisposable
	{
		IEcsSystems _initSystems;
		IEcsSystems _updateSystem;
		IEcsSystems _fixedUpdateSystem;
		IEcsSystems _lateUpdateSystem;
		IEcsSystems _cleanupSystem;

		readonly ISystemFactory _systemFactory;

		protected Feature(ISystemFactory systemFactory)
		{
			_systemFactory = systemFactory;
		}

		public void Init()
		{
			Init(_initSystems);
			Init(_updateSystem);
			Init(_fixedUpdateSystem);
			Init(_lateUpdateSystem);
			Init(_cleanupSystem);
		}

		public void Start() =>
			Run(_initSystems);

		public void Update() =>
			Run(_updateSystem);

		public void FixedUpdate() =>
			Run(_fixedUpdateSystem);

		public void LateUpdate() =>
			Run(_lateUpdateSystem);

		public void Cleanup() =>
			Run(_cleanupSystem);

		public void Dispose()
		{
			DestroySystems(ref _updateSystem);
			DestroySystems(ref _fixedUpdateSystem);
			DestroySystems(ref _lateUpdateSystem);
		}

		protected void AddInit<TSystem>() where TSystem : class, IEcsInitSystem =>
			Add<TSystem>(ref _initSystems);

		protected void AddUpdate<TSystem>() where TSystem : class, IEcsRunSystem =>
			Add<TSystem>(ref _updateSystem);

		protected void AddFixedUpdate<TSystem>()
			where TSystem : class, IEcsRunSystem =>
			Add<TSystem>(ref _fixedUpdateSystem);

		protected void AddLateUpdate<TSystem>()
			where TSystem : class, IEcsRunSystem =>
			Add<TSystem>(ref _lateUpdateSystem);

		protected void AddCleanup<TSystem>()
			where TSystem : class, IEcsRunSystem =>
			Add<TSystem>(ref _cleanupSystem);

		void Add<TSystem>(ref IEcsSystems systems)
			where TSystem : class, IEcsSystem
		{
			systems ??= _systemFactory.CreateSystemGroup();
			systems.Add(_systemFactory.Create<TSystem>());
		}

		void Init(IEcsSystems systems) => 
			systems?.Inject().Init();

		void Run(IEcsSystems systems)
		{
			systems?.Run();
		}

		void DestroySystems(ref IEcsSystems systems)
		{
			systems?.Destroy();
			systems = null;
		}
	}
}