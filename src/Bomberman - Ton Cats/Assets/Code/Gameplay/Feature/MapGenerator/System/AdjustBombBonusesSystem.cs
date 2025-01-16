using System.Collections.Generic;
using Common.Component;
using Gameplay.Feature.Bomb;
using Gameplay.Feature.Bonus.Component;
using Gameplay.Feature.Bonus.StaticData;
using Gameplay.Progress;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.MapGenerator.System
{
	public sealed class AdjustBombBonusesSystem : IEcsRunSystem
	{
		[Inject] IBonusNames _bonusNames;
		[Inject] EntityWrapper _bonus;
		[Inject] IProgressService _progress;
		[Inject] IAdditionalBombBonuses _additionalBombBonuses;

		readonly EcsFilterInject<Inc<BonusComponent, BonusType, FirstBreath>>
			_bonusFilterInject;

		public void Run(IEcsSystems systems)
		{
			if (TryGetBombDataList(out var bombBonusesDataList) == false)
				return;

			foreach (var bonusEntity in _bonusFilterInject.Value)
			{
				_bonus.SetEntity(bonusEntity);
				var bonusType = _bonus.BonusType();
				if (bonusType != _bonusNames.Bomb)
					continue;

				if (bombBonusesDataList.Count == 0)
				{
					Debug.LogError("Has no data to create the bomb bonus.");
					return;
				}
				
				var bombType = bombBonusesDataList[0];
				bombBonusesDataList.RemoveAt(0);
				_bonus.AddBombBonusType(bombType);
			}
		}

		bool TryGetBombDataList(out List<BombType> list)
		{
			list = new();

			if (false == _additionalBombBonuses
				    .TryGetBombs(_progress.ReachedLevel, out var bombsData))
			{
				Debug.LogError("Cannot to get bomb bonuses.");
				return false;
			}

			foreach (var pair in bombsData)
				for (int i = 0; i < pair.Value; i++)
					list.Add(pair.Key);

			return true;
		}
	}
}