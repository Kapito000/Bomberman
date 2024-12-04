using Gameplay.Audio.Service;
using Gameplay.Feature.MainMenuMusic.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.MainMenuMusic.System
{
	public sealed class CreateMainMenuMusiSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _entity;
		
		[Inject] IAudioService _audioService;
		[Inject] IMainMenuMusicFactory _musicFactory;
		
		public void Run(IEcsSystems systems)
		{
			var e = _musicFactory.CreateAmbientMusic();
			_entity.SetEntity(e);
			var audioSource = _entity.AudioSource();
			audioSource.Play();
		}
	}
}