using Factory.SystemFactory;
using Feature.Bomb.System;

namespace Feature.Bomb
{
	public sealed class BombFeature : Infrastructure.ECS.Feature
	{
		public BombFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateBombParentSystem>();
			
			AddUpdate<PutBombSystem>();
			AddUpdate<BombExplosionSystem>();
			AddUpdate<StartExplosionSystem>();
			AddUpdate<ScanExplosionAreaSystem>();
			AddUpdate<CreateBlowUpDestructibleSystem>();
			AddUpdate<CreateExplosionCenterSystem>();
			AddUpdate<CreateExplosionPartSystem>();
			
			AddCleanup<CreateExplosionRequestCleanupSystem>(); 
		}
	}
}