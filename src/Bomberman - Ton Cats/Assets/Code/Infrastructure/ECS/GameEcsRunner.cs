using AB_Utility.FromSceneToEntityConverter;
using Gameplay.Feature;
using Gameplay.LevelData;
using Infrastructure.Factory.SystemFactory;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Infrastructure.ECS
{
	public sealed class GameEcsRunner : MonoBehaviour, IEcsRunner
	{
		[Inject] EcsWorld _world;
		[Inject] ILevelData _levelData;
		[Inject] ISystemFactory _systemFactory;
		[Inject] FeatureController _features;

		IEcsSystems _supprotiveSystems;

		public void InitWorld()
		{
			_supprotiveSystems = new EcsSystems(_world);
			_supprotiveSystems
#if UNITY_EDITOR
				.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
				.Add(new Mitfart.LeoECSLite.UnityIntegration.EcsWorldDebugSystem())
#endif
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