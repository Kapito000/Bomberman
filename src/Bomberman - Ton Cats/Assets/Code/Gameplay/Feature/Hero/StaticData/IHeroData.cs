using Gameplay.StaticData;
using UnityEngine.U2D.Animation;

namespace Gameplay.Feature.Hero.StaticData
{
	public interface IHeroData : IStaticData
	{
		float MovementSpeed { get; }
		int StartBombNumber { get; }
		int LifePointsOnStart { get; }
		SpriteLibraryAsset SkinLibrary();
	}
}