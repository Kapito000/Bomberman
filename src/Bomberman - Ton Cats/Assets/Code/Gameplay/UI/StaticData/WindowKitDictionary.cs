using System;
using Common;
using Gameplay.Windows;

namespace Gameplay.UI.StaticData
{
	[Serializable]
	public sealed class
		WindowKitDictionary : SerializedDictionary<WindowKitId, WindowIdCollection>
	{ }

	[Serializable]
	public struct WindowIdCollection
	{
		public WindowId[] Values;
	}
}