using Zenject;

namespace Infrastructure.Installer
{
	public interface IInstallBindings
	{
		void Bind(DiContainer container);
	}
}