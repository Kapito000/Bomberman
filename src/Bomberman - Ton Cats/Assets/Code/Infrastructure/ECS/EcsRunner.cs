using AB_Utility.FromSceneToEntityConverter;
using Factory.SystemFactory;
using Feature;
using Leopotam.EcsLite;
using LevelData;
using UnityEngine;
using Zenject;

namespace Infrastructure.ECS
{
	public sealed class EcsRunner : MonoBehaviour, IEcsRunner
	{
		[Inject] EcsWorld _world;
		[Inject] ILevelData _levelData;
		[Inject] ISystemFactory _systemFactory;
		[Inject] FeatureController _features;

		IEcsSystems _worldDebugSystems;

		public void InitWorld()
		{
			_worldDebugSystems = new EcsSystems(_world);
			_worldDebugSystems
				.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
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
			_worldDebugSystems?.Run();
		}

		void OnDestroy()
		{
			_features.Dispose();
			_worldDebugSystems?.Destroy();
			_world?.Destroy();
		}
	}
}