using Infrastructure;
using UnityEngine.U2D.Animation;

namespace Gameplay.Reskin
{
	public interface IReskinService : IService
	{
		SpriteLibraryAsset GetSkinSpriteLibraryAsset(string key);
	}
}