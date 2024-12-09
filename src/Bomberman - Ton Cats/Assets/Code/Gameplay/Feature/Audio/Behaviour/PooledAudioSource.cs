using Extensions;
using Gameplay.Audio;
using Gameplay.Audio.Service;
using Infrastructure.ECS;
using Infrastructure.Factory.EntityBehaviourFactory;
using UnityEngine;
using Zenject;

namespace Gameplay.Feature.Audio.Behaviour
{
	public sealed class PooledAudioSource : MonoBehaviour
	{
		[Inject] Pool _pool;

		public sealed class Pool : MemoryPool<PooledAudioSource>
		{
			[Inject] IAudioService _audioService;
			[Inject] IEntityBehaviourFactory _entityFactory;
			[Inject] EntityWrapper _entity;

			public bool SpawnAndTryGetEntity(out int entity)
			{
				PooledAudioSource item = Spawn();
				if (TryGetEntity(out entity, item) == false)
					return false;

				return true;
			}

			protected override void OnCreated(PooledAudioSource item)
			{
				var audioSource = _audioService.ReplaceAudioSource(item.gameObject);
				_audioService.AssignMixerGroup(MixerGroup.SFX, audioSource);
				item.gameObject.SetActive(false);

				var e = _entityFactory.InitEntityBehaviour(item.gameObject);
				_entity.SetEntity(e);
				_entity
					.AddPooledAudioSourceComponent(item)
					.AddPooledAudioSourcePool(this)
					.AddAudioSource(audioSource)
					.AddTransform(item.transform)
					;
			}

			protected override void OnSpawned(PooledAudioSource item)
			{
				item.gameObject.SetActive(true);
			}

			protected override void OnDespawned(PooledAudioSource item)
			{
				item.gameObject.SetActive(false);
			}

			bool TryGetEntity(out int entity, PooledAudioSource item)
			{
				if (TyrGetEntityBehaviour(item, out var entityBehaviour) == false)
				{
					Despawn(item);
					entity = default;
					return false;
				}

				if (entityBehaviour.TryGetEntity(out entity) == false)
				{
					Despawn(item);
					return false;
				}
				return true;
			}

			bool TyrGetEntityBehaviour(PooledAudioSource item,
				out EntityBehaviour entityBehaviour)
			{
				if (item.TyrGetEntityBehaviour(out entityBehaviour) == false)
				{
					Debug.LogError(
						$"Cannot spawn via the \"{nameof(PooledAudioSource)}\" " +
						$"has no {nameof(entityBehaviour)}.");
					return false;
				}
				return true;
			}
		}
	}
}