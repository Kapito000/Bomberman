using System;
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
			DestroyWorld();
		}

		void DestroyWorld()
		{
#if !UNITY_EDITOR
			_world?.Destroy();
#endif
#if UNITY_EDITOR
			try
			{
				_world?.Destroy();
			}
			catch (Exception e)
			{
				Debug.LogWarning($"Need to refactoring the " +
					$"\"Mitfart.LeoECSLite.UnityIntegration\".\n" + e);
			}
#endif
		}
	}
}