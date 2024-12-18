namespace Gameplay.AI.Navigation
{
	public sealed class AINavigationSurface : INavigationSurface
	{
		readonly NavMeshSurfaceIdDictionary _surfaces;

		public AINavigationSurface(NavMeshSurfaceIdDictionary surfaces)
		{
			_surfaces = surfaces;
			HideEditorLogs();
		}

		public void Bake()
		{
			foreach (var surface in _surfaces.Values)
				surface.BuildNavMesh();
		}

		public void Update()
		{
			foreach (var surface in _surfaces.Values)
				surface.UpdateNavMesh(surface.navMeshData);
		}

		void HideEditorLogs()
		{
			foreach (var surface in _surfaces.Values)
				surface.hideEditorLogs = true;
		}
	}
}