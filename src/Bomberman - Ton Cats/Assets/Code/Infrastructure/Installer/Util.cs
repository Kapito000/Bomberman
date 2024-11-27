using Zenject;

namespace Infrastructure.Installer
{
	public static class Util
	{
		public static void ResolveDiContainerDependences(DiContainer container)
		{
			foreach (var dependence in container.Resolve<IDiContainerDependence[]>())
				dependence.SetContainer(container);
		}
	}
}