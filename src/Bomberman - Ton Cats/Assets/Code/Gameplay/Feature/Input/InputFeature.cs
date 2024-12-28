using Gameplay.Feature.Input.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Input
{
	public sealed class InputFeature : Infrastructure.ECS.Feature
	{
		public InputFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CharacterPutBombInputSystem>();
			
			AddInit<CharacterScreenTapSystem>();
			
			AddUpdate<CharacterMoveInputSystem>();
			
			AddCleanup<CleanupSystem>();
		}
	}
}