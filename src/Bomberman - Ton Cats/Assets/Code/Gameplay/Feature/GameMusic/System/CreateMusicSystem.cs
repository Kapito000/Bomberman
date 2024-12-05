using Common.Component;
using Gameplay.Feature.GameMusic.Component;
using Gameplay.Feature.GameMusic.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.GameMusic.System
{
	public sealed class CreateMusicSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _parent;
		[Inject] EntityWrapper _gameMusic;
		[Inject] IGameMusicFactory _gameMusicFactory;

		readonly EcsFilterInject<
			Inc<MusicParent, TransformComponent>> _parentFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var parentEntity in _parentFilter.Value)
			{
				_parent.SetEntity(parentEntity);
				var parent = _parent.Transform();

				var music = _gameMusicFactory.CreateGameMusic(parent);
				_gameMusic.SetEntity(music);
				var audioSource = _gameMusic.AudioSource();
				audioSource.Play();
			}
		}
	}
}