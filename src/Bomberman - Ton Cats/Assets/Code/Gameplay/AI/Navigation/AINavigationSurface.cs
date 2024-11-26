using NavMeshPlus.Components;

namespace Gameplay.AI.Navigation
{
	public sealed class AINavigationSurface : INavigationSurface
	{
		readonly NavMeshSurface _navMeshSurface;

		public AINavigationSurface(NavMeshSurface navMeshSurface)
		{
			_navMeshSurface = navMeshSurface;
			navMeshSurface.hideEditorLogs = true;
		}

		public void Bake()
		{
			_navMeshSurface.BuildNavMesh();
		}

		public void Update()
		{
			_navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
		}
	}
}