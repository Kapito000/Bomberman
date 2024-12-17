using Gameplay.StaticData;
using UnityEngine.U2D.Animation;

namespace Gameplay.Reskin.StaticData
{
	public interface ISkinLibrary : IStaticData
	{
		bool TryGet(string key, out SpriteLibraryAsset library);
		bool TryGetWithDebug(string key, out SpriteLibraryAsset library);
	}
}