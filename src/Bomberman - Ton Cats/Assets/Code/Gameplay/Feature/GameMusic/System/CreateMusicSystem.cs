using Gameplay.Feature.GameMusic.Factory;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Zenject;

namespace Gameplay.Feature.GameMusic.System
{
	public sealed class CreateMusicSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _entity;

		[Inject] IGameMusicFactory _gameMusicFactory;

		public void Run(IEcsSystems systems)
		{
			var e = _gameMusicFactory.CreateGameMusic();
			_entity.SetEntity(e);
			var audioSource = _entity.AudioSource();
			audioSource.Play();
		}
	}
}