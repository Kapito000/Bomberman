using Factory.EntityBehaviourFactory;
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
		[Inject] IEntityBehaviourFactory _entityBehaviourFactory;

		public int CreateHero(Vector2 pos, Quaternion rot, Transform parent)
		{
			var prefab = _assetProvider.Hero();
			var heroObj = _instantiator.Instantiate(prefab, pos, rot, parent);
			return _entityBehaviourFactory.CreateEntityBehaviour(heroObj);
		}
	}
}