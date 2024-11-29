using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.FinishLevel.Factory
{
	public interface IFinishLevelFactory : IFactory
	{
		int CreateFinishLevelObserver();
		int CreateFinishLevelDoor(Vector2 pos);
	}
}