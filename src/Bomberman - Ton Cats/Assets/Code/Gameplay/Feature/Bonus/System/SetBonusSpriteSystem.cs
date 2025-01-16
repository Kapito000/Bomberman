using Common.Component;
using Gameplay.Feature.Bonus.Component;
using Gameplay.Feature.Bonus.StaticData;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Bonus.System
{
	public sealed class SetBonusSpriteSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _bonus;
		[Inject] IBonusSprites _bonusSprites;

		readonly EcsFilterInject<
				Inc<BonusComponent, BonusType, SpriteRendererComponent>>
			_bonusesFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var bonusEntity in _bonusesFilter.Value)
			{
				_bonus.SetEntity(bonusEntity);
				var bonusType = _bonus.BonusType();
				var spriteRenderer = _bonus.SpriteRenderer();
				if (_bonusSprites.TryGetSprite(bonusType, out var sprite) == false)
				{
					Debug.LogError($"Cannot to get sprite for bonus: \"{bonusType}\".");
					continue;
				}
				
				spriteRenderer.sprite = sprite;
			}
		}
	}
}