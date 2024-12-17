using UnityEngine;
using UnityEngine.U2D.Animation;
using Menu = Constant.CreateAssetMenu.Path;

namespace Gameplay.Reskin.StaticData
{
	[CreateAssetMenu(menuName = Menu.c_StaticData + nameof(SkinLibrary))]
	public sealed class SkinLibrary : ScriptableObject, ISkinLibrary
	{
		[SerializeField] SkinDictionary _dictionary;

		public bool TryGet(string key, out SpriteLibraryAsset library) =>
			_dictionary.TryGetValue(key, out library);

		public bool TryGetWithDebug(string key, out SpriteLibraryAsset library)
		{
			if (TryGet(key, out library) == false)
			{
				Debug.LogError($"Cannot to find the \"{key}\" skin library.");
				return false;
			}
			return true;
		}
	}
}