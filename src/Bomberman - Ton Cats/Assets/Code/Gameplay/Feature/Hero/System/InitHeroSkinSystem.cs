using Common.Component;
using Gameplay.Feature.Hero.Component;
using Gameplay.Feature.Hero.StaticData;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Hero.System
{
	public sealed class InitHeroSkinSystem : IEcsRunSystem
	{
		[Inject] IHeroData _heroData;
		[Inject] EntityWrapper _hero;

		readonly EcsFilterInject<
			Inc<HeroComponent, SpriteLibraryComponent>> _filter;

		public void Run(IEcsSystems systems)
		{
			foreach (var hero in _filter.Value)
			{
				_hero.SetEntity(hero);
				var skinLibrary = _heroData.SkinLibrary();
				var spriteLibrary = _hero.SpriteLibrary();
				spriteLibrary.spriteLibraryAsset = skinLibrary;
			}
		}
	}
}