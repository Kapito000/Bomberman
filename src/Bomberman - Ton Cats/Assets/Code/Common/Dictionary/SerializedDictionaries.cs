using System;
using UnityEngine;

namespace Common.Dictionary
{
	[Serializable]
	public class StringIntegerDictionary : SerializedDictionary<string, int>
	{ }

	[Serializable]
	public class StringSpriteDictionary : SerializedDictionary<string, Sprite>
	{ }
}