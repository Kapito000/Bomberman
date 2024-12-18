using System;
using Common.Dictionary;
using NavMeshPlus.Components;

namespace Gameplay.AI.Navigation
{
	[Serializable]
	public sealed class NavMeshSurfaceIdDictionary : SerializedDictionary
		<NavigationSurfaceId, NavMeshSurface>
	{ }
}