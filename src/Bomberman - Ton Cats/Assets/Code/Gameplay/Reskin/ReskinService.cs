using Gameplay.Reskin.StaticData;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Zenject;

namespace Gameplay.Reskin
{
	public sealed class ReskinService : IReskinService
	{
		[Inject] ISkinLibrary _skinLibrary;

		public SpriteLibraryAsset GetSkinSpriteLibraryAsset(string key)
		{
			if (_skinLibrary.TryGet(key, out var result) == false)
			{
				Debug.LogWarning($"Cannot to find the \"{key}\" skin library. " +
					$"Was returned default skin.");
				_skinLibrary.TryGet(Constant.SkinName.c_Default, out result);
			}

			return result;
		}
	}
}