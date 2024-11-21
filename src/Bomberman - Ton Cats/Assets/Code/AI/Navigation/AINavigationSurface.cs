using NavMeshPlus.Components;
using Zenject;

namespace AI.Navigation
{
	public sealed class AINavigationSurface : INavigationSurface
	{
		[Inject] NavMeshSurface _navMeshSurface;

		public void Bake()
		{
			_navMeshSurface.BuildNavMesh();
		}
	}
}