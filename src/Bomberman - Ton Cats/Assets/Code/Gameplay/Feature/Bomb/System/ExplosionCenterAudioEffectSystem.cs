using Common.Component;
using Gameplay.Audio.Service;
using Gameplay.Feature.Bomb.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Bomb.System
{
	public sealed class ExplosionCenterAudioEffectSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _explosion;
		[Inject] IAudioService _audioService;

		readonly EcsFilterInject<
				Inc<Explosion, FirstBreath, ExplosionPartComponent, TransformComponent>>
			_explosionFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var e in _explosionFilter.Value)
			{
				_explosion.SetEntity(e);
				var explosionPart = _explosion.ExplosionPart();
				if (explosionPart != ExplosionPart.Center)
					continue;

				var pos = _explosion.TransformPos();
				var clipId = Constant.AudioClipId.c_BombExplosion;
				_audioService.Player.PlaySfxClipAtPoint(clipId, pos);
			}
		}
	}
}