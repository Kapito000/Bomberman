using System;
using Common;
using Gameplay.Windows;

namespace Infrastructure.AssetProvider
{
	[Serializable]
	public sealed class WindowsDictionary : SerializedDictionary<WindowId, BaseWindow>
	{ }
}