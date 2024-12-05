using Gameplay.Audio;
using Gameplay.Audio.Factory;
using Gameplay.Feature.GameMusic.Component;
using Infrastructure.ECS;
using Infrastructure.Factory.Kit;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.GameMusic.Factory
{
	public sealed class GameMusicFactory : IGameMusicFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] EntityWrapper _entity;
		[Inject] IMusicFactory _musicFactory;

		public int CreateGameMusic(Transform parent)
		{
			var prefab = _kit.AssetProvider.GameMusic();
			return _musicFactory.CreateAmbientMusic(AmbientMusic.Game, prefab, parent);
		}

		public int CreateMusicParent()
		{
			var obj = new GameObject(Constant.ObjectName.c_MusicParent);
			var e = _kit.EntityBehaviourFactory.InitEntityBehaviour(obj);
			_entity.SetEntity(e);
			_entity
				.Add<MusicParent>()
				.AddTransform(obj.transform)
				;
			return e;
		}
	}
}