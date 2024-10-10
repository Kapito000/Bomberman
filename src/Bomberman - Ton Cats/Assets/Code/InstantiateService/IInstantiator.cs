using Infrastructure;
using UnityEngine;

namespace InstantiateService
{
	public interface IInstantiator : IService
	{
		GameObject Instantiate(Object prefab);

		GameObject Instantiate(GameObject prefab, Vector2 pos, Quaternion rot,
			Transform parent);

		TComponent AddComponent<TComponent>(GameObject instance)
			where TComponent : Component;
	}
}