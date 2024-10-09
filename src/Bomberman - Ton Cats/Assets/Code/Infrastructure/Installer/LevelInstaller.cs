using Infrastructure.SystemFactory;
using Leopotam.EcsLite;
using Zenject;

namespace Infrastructure.Installer
{
	public class LevelInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindWorld();
			BindSystemFactory();
		}

		void BindSystemFactory()
		{
			Container.Bind<ISystemFactory>().To<StandardSystemFactory>().AsSingle();
		}

		void BindWorld()
		{
			Container.Bind<EcsWorld>().FromNew().AsSingle();
			Container.Bind<EcsSystems>().FromNew().AsSingle();
		}
	}
}