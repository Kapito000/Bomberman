using Infrastructure.AssetProvider;
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
			return instance;
		}
	}
}