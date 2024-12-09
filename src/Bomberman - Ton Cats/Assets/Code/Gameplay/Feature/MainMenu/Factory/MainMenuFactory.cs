using Gameplay.Audio;
using Gameplay.Audio.Factory;
using Gameplay.Feature.MainMenu.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.MainMenu.Factory
{
	public sealed class MainMenuFactory : IMainMenuFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _entity;
		[Inject] IMusicFactory _musicFactory;

		public int CreateUpperPanel(Transform parent)
		{
			var prefab = _kit.AssetProvider.MainMenuUpperPanel();
			var instance = _kit.InstantiateService.Instantiate(prefab, parent);
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(instance);
			_entity.SetEntity(e);
			_entity
				.AddTransform(instance.transform)
				.Add<MainMenuUpperPanel>()
				;
			return e;
		}

		public int CreateAmbientMusic(Transform parent)
		{
			var prefab = _kit.AssetProvider.GameMusic();
			return _musicFactory
				.CreateAmbientMusic(AmbientMusic.MainMenu, prefab, parent);
		}
	}
}