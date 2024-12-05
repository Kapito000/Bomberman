using Common;
using UnityEngine;

namespace Gameplay.Audio.Library
{
	public abstract class AudioClipLibrary<TKey> : SerializedDictionary<TKey, AudioClip>
	{ }
}