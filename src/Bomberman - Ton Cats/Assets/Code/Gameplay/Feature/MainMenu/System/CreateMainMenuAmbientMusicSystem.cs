using Common.Component;
using Gameplay.Audio.Service;
using Gameplay.Feature.GameMusic.Component;
using Gameplay.Feature.MainMenu.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.MainMenu.System
{
	public sealed class CreateMainMenuAmbientMusicSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _musicEntity;
		[Inject] EntityWrapper _parentEntity;
		[Inject] IAudioService _audioService;
		[Inject] IMainMenuFactory _factory;

		readonly EcsFilterInject<
			Inc<MusicParent, TransformComponent>> _parentFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var parentEntity in _parentFilter.Value)
			{
				_parentEntity.SetEntity(parentEntity);
				var parent = _parentEntity.Transform();

				var musicEntity = _factory.CreateAmbientMusic(parent);
				_musicEntity.SetEntity(musicEntity);
				var audioSource = _musicEntity.AudioSource();
				audioSource.Play();
			}
		}
	}
}