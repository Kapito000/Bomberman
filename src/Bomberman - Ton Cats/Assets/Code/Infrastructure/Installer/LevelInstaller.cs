using Factory.SystemFactory;
using Feature;
using Infrastructure.Boot;
using Infrastructure.ECS;
using Infrastructure.GameStatus;
using Infrastructure.GameStatus.State;
using Leopotam.EcsLite;
using LevelData;
using UnityEngine;
using Zenject;

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
			BindSystemFactory();
			FeatureController();
			BindDevSceneRunner();
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

		void FeatureController()
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