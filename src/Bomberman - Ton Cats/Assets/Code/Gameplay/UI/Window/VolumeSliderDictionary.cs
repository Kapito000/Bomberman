using System;
using Common;
using Common.Dictionary;
using Gameplay.Audio;
using UnityEngine.UI;

namespace Gameplay.UI.Window
{
	[Serializable]
	public sealed class
		VolumeSliderDictionary : SerializedDictionary<VolumeType, Slider>
	{ }
}