using System;
using Common;
using UnityEngine.Audio;

namespace Gameplay.Audio.MixerProvider
{
	[Serializable]
	public sealed class MixerGroupDictionary :
		SerializedDictionary<MixerGroup, AudioMixerGroup>
	{ }
}