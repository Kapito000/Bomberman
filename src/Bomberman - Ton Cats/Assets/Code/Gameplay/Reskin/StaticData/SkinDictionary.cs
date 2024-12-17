using System;
using Common.Dictionary;
using UnityEngine.U2D.Animation;

namespace Gameplay.Reskin.StaticData
{
	[Serializable]
	public sealed class SkinDictionary : SerializedDictionary<string, SpriteLibraryAsset>
	{ }
}