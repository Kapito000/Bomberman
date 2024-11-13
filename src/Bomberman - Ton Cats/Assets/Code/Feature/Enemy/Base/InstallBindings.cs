using Feature.Enemy.Base.System;
using Infrastructure.Installer;
using Zenject;

namespace Feature.Enemy.Base
{
	public sealed class InstallBindings : IInstallBindings
	{
		public void Bind(DiContainer container)
		{
			container.Bind<Patrolling>().AsSingle();
		}
	}
}