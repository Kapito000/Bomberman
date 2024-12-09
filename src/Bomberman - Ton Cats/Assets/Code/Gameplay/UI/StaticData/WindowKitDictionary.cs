using System;
using Common;
using Gameplay.Windows;

namespace Gameplay.UI.StaticData
{
	[Serializable]
	public sealed class
		WindowKitDictionary : SerializedDictionary<WindowKitId, WindowId[]>
	{ }
}