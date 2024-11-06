using AB_Utility.FromSceneToEntityConverter;
using Factory.SystemFactory;
using Feature;
using Leopotam.EcsLite;
using LevelData;
using Mitfart.LeoECSLite.UnityIntegration;
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
				// .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
				.Add(new EcsWorldDebugSystem(null, new NameSettings(true)))
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