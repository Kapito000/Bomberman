﻿using UnityEngine;
using Zenject;

namespace InstantiateService
{
	public sealed class StandardInstantiator : IInstantiator
	{
		[Inject] DiContainer _container;

		public GameObject Instantiate(Object prefab)
		{
			var instance = _container.InstantiatePrefab(prefab);
			return instance;
		}

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