using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Infrastructure.InstantiateService
{
	public sealed class StandardInstantiateService : IInstantiateService
	{
		[Inject] DiContainer _container;

		public TComponent Instantiate<TComponent>(Object prefab)
			where TComponent : Component =>
			_container.InstantiatePrefabForComponent<TComponent>(prefab);

		public TComponent Instantiate<TComponent>(Object prefab, Transform parent)
			where TComponent : Component =>
			_container.InstantiatePrefabForComponent<TComponent>(prefab, parent);

		public GameObject Instantiate(Object prefab)
		{
			var instance = _container.InstantiatePrefab(prefab);
			return instance;
		}

		public GameObject Instantiate(Object prefab, Vector2 pos) =>
			_container.InstantiatePrefab(prefab, pos, quaternion.identity, null);

		public GameObject Instantiate(Object prefab, Transform parent)
		{
			return _container.InstantiatePrefab(prefab, parent);
		}

		public GameObject Instantiate(GameObject prefab, Vector2 pos,
			Transform parent) =>
			Instantiate(prefab, pos, quaternion.identity, parent);

		public GameObject Instantiate(GameObject prefab,
			Vector2 pos = new(), Quaternion rot = new(),
			Transform parent = null)
		{
			var instance = _container
				.InstantiatePrefab(prefab, pos, rot, parent);
			return instance;
		}

		public TComponent AddComponent<TComponent>(GameObject instance)
			where TComponent : Component
		{
			return _container.InstantiateComponent<TComponent>(instance);
		}
	}
}