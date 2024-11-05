using Infrastructure;
using UnityEngine;

namespace InstantiateService
{
	public interface IInstantiateService : IService
	{
		GameObject Instantiate(Object prefab);
		GameObject Instantiate(Object prefab, Transform parent);

		GameObject Instantiate(GameObject prefab, Vector2 pos, Transform parent);

		GameObject Instantiate(GameObject prefab, Vector2 pos, Quaternion rot,
			Transform parent);

		TComponent AddComponent<TComponent>(GameObject instance)
			where TComponent : Component;

		TComponent Instantiate<TComponent>(Object prefab)
			where TComponent : Component;
		
		TComponent Instantiate<TComponent>(Object prefab, Transform parent)
			where TComponent : Component;
	}
}