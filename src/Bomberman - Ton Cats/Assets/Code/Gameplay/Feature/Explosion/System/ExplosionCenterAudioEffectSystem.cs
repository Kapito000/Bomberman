using Common.Component;
using Gameplay.Audio.Service;
using Gameplay.Feature.Explosion.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Gameplay.Feature.Explosion.System
{
	public sealed class ExplosionCenterAudioEffectSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _explosion;
		[Inject] IAudioService _audioService;
		
		readonly EcsFilterInject<
				Inc<Component.Explosion, ExplosionCenter, FirstBreath, TransformComponent>>
			_explosionFilter;
		
		public void Run(IEcsSystems systems)
		{
			foreach (var e in _explosionFilter.Value)
			{
				_explosion.SetEntity(e);
				var pos = _explosion.TransformPos();
				var clipId = Constant.AudioClipId.c_BombExplosion;
				_audioService.Player.PlaySfxClipAtPoint(clipId, pos);
			}
		}
	}
}