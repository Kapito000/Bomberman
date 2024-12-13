using System;
using Common;
using Common.Dictionary;
using UnityEngine.Audio;

namespace Gameplay.Audio.MixerGroupProvider
{
	[Serializable]
	public sealed class MixerGroupDictionary :
		SerializedDictionary<MixerGroup, AudioMixerGroup>
	{ }
}