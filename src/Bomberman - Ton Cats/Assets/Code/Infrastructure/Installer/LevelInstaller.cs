using Factory.CameraFactory;
using Factory.HeroFactory;
using Factory.SystemFactory;
using Feature;
using Infrastructure.Boot;
using Infrastructure.ECS;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using InstantiateService;
using Leopotam.EcsLite;
using LevelData;
using UnityEngine;
using Zenject;
using IInstantiator = InstantiateService.IInstantiator;

namespace Infrastructure.Installer
{
	public class LevelInstaller : MonoInstaller, IInitializable
	{
		[SerializeField] EcsRunner _ecsRunner;

		[Inject] ILevelData _levelData;
		[Inject] IGameStateMachine _gameStateMachine;

		public override void InstallBindings()
		{
			BindInitializable();
			BindWorld();
			BindFactories();
			BindInstantiator();
			BindSystemFactory();
			BindDevSceneRunner();
			BindFeatureController();
		}

		public void Initialize()
		{
			InitLevelData();
			_gameStateMachine.Enter<LaunchGame>();
		}

		void InitLevelData()
		{
			_levelData.EcsRunner = _ecsRunner;
			_levelData.DevSceneRunner = Container.Resolve<IDevSceneRunner>();
		}

		void BindDevSceneRunner()
		{
			Container.Bind<IDevSceneRunner>().FromComponentInHierarchy().AsSingle();
		}

		void BindInstantiator()
		{
			Container.Bind<IInstantiator>().To<StandardInstantiator>().AsSingle();
		}

		void BindFactories()
		{
			Container.Bind<ICameraFactory>().To<CameraFactory>().AsSingle();
			Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
		}

		void BindFeatureController()
		{
			Container.Bind<FeatureController>().AsSingle();
		}

		void BindSystemFactory()
		{
			Container.Bind<ISystemFactory>().To<StandardSystemFactory>().AsSingle();
		}

		void BindWorld()
		{
			Container.Bind<EcsWorld>().FromNew().AsSingle();
		}

		void BindInitializable()
		{
			Container.BindInterfacesTo<LevelInstaller>().FromInstance(this)
				.AsSingle();
		}
	}
}