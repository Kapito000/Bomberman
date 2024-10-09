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
		[Inject] EcsSystems _systems;
		[Inject] ISystemFactory _systemFactory;

		void Start()
		{
			_systems.Add(_systemFactory.Create<GameFeature>())
#if UNITY_EDITOR
				.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
				.Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif
				.Init();
		}

		void Update()
		{
			_systems?.Run();
		}

		void OnDestroy()
		{
			_systems?.Destroy();
			_world?.Destroy();
		}
	}
}