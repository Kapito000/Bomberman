using AB_Utility.FromSceneToEntityConverter;
using Gameplay.Feature.FeatureControl;
using Gameplay.LevelData;
using Infrastructure.Factory.SystemFactory;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;
using Mitfart.LeoECSLite.UnityIntegration;

namespace Infrastructure.ECS
{
	public sealed class EcsRunner : MonoBehaviour, IEcsRunner
	{
		[Inject] EcsWorld _world;
		[Inject] ILevelData _levelData;
		[Inject] ISystemFactory _systemFactory;
		[Inject] IFeatureController _features;

		IEcsSystems _supprotiveSystems;

		public void InitWorld()
		{
			Debug.Log("Init world");
			_supprotiveSystems = new EcsSystems(_world);
#if UNITY_EDITOR
			_levelData.EcsWorldDebugSystem = new EcsWorldDebugSystem();
			_supprotiveSystems.Add(_levelData.EcsWorldDebugSystem);
#endif
			_supprotiveSystems
				.ConvertScene()
				.Init();

			_features.Init();
			_features.Start();
			enabled = true;
		}

		void Update()
		{
			_features.Update();
		}

		void FixedUpdate()
		{
			_features.FixedUpdate();
		}

		void LateUpdate()
		{
			_features.LateUpdate();
			_features.Cleanup();
			_supprotiveSystems?.Run();
		}

		void OnDestroy()
		{
			_features.Dispose();
			_supprotiveSystems?.Destroy();
			_world?.Destroy();
		}
	}
}