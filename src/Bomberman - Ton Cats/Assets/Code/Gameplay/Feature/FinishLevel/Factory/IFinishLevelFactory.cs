using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.FinishLevel.Factory
{
	public interface IFinishLevelFactory : IFactory
	{
		int CreateFinishLevelObserver();
		GameObject CreateFinishLevelDoor(int doorEntity, Vector2 pos);
		int CreateFinishLevelDoorEntity(Vector2Int cell);
		int CreateFinishLevelMusic(Transform parent);
	}
}