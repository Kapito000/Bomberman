using Infrastructure.AssetProvider;
using Infrastructure.ECS;
using UnityEngine;
using Zenject;
using IInstantiator = InstantiateService.IInstantiator;

namespace Factory.HeroFactory
{
	public sealed class HeroFactory : IHeroFactory
	{
		[Inject] IInstantiator _instantiator;
		[Inject] IAssetProvider _assetProvider;

		public GameObject CreateHero(Vector2 pos, Quaternion rot, Transform parent)
		{
			var prefab = _assetProvider.Hero();
			var instance = _instantiator.Instantiate(prefab, pos, rot, parent);

			if (!instance.TryGetComponent<EntityBehaviour>(out var entityBehaviour))
				_instantiator.AddComponent<EntityBehaviour>(instance);

			return instance;
		}
	}
}