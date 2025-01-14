using UnityEngine;
using Zenject;
using Menu = Constant.CreateAssetMenu;

namespace Infrastructure.Installer
{
	[CreateAssetMenu(menuName =
		Menu.Path.c_Installers + nameof(TsvDataInstaller))]
	public sealed class TsvDataInstaller : ScriptableObjectInstaller
	{
		[SerializeField] TextAsset _bombData;

		public override void InstallBindings()
		{
			Container.Bind<TextAsset>().WithId(TsvData.BombData)
				.FromInstance(_bombData).AsSingle();
		}
	}
}