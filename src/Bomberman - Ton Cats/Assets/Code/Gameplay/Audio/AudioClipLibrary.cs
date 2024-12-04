using Common;
using UnityEngine;

namespace Gameplay.Audio
{
	public abstract class AudioClipLibrary<TKey> : SerializedDictionary<TKey, AudioClip>
	{ }
}