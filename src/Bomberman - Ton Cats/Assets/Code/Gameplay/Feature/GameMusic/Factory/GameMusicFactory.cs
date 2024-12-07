﻿using Gameplay.Audio;
using Gameplay.Audio.Factory;
using Infrastructure.Factory.Kit;
using Zenject;

namespace Gameplay.Feature.GameMusic.Factory
{
	public sealed class GameMusicFactory : IGameMusicFactory
	{
		[Inject] IFactoryKit _kit;
		[Inject] IMusicFactory _musicFactory;

		public int CreateGameMusic()
		{
			var prefab = _kit.AssetProvider.GameMusicPrefab();
			return _musicFactory.CreateAmbientMusic(AmbientMusic.Game, prefab);
		}
	}
}