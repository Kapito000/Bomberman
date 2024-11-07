using System;
using Windows;
using Common;

namespace Infrastructure.AssetProvider
{
	[Serializable]
	public sealed class WindowsDictionary : SerializedDictionary<WindowId, BaseWindow>
	{ }
}