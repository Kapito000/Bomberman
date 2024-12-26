using Gameplay.Feature.Bomb.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Bomb
{
	public sealed class BombFeature : Infrastructure.ECS.Feature
	{
		public BombFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateBombParentSystem>();
			
			AddUpdate<PutBombSystem>();
			AddUpdate<PutBombAudioEffect>();

			AddUpdate<ExplosionTimerSystem>();
			AddUpdate<BombExplosionSystem>();
			
			AddUpdate<CallExplosionSystem>();
			AddUpdate<ScanExplosionAreaSystem>();
			AddUpdate<CreateBlowUpDestructibleSystem>();
			AddUpdate<CreateExplosionSystem>();
			AddUpdate<ExplosionCenterAudioEffectSystem>();
			
			AddUpdate<AddToTargetBufferSystem>();
			AddUpdate<RemoveFromTargetBufferSystem>();
			AddUpdate<ExplosionDamageSystem>();
		}
	}
}