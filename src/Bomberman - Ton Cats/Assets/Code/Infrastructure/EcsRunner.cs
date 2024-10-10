using Feature;
using Infrastructure.SystemFactory;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
	public sealed class EcsRunner : MonoBehaviour
	{
		[Inject] EcsWorld _world;
		[Inject] ISystemFactory _systemFactory;
		[Inject] FeatureController _features;

		EcsSystems _worldDebugSystems;

		void InitWorld()
		{
#if UNITY_EDITOR
			_worldDebugSystems = new EcsSystems(_world);
			_worldDebugSystems
				.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
				.Init();
#endif
			_features.Init();
			_features.Start();
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