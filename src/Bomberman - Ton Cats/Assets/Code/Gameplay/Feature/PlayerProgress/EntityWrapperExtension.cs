using Gameplay.Feature.PlayerProgress.Component;
using Infrastructure.ECS;

namespace Gameplay.Feature.PlayerProgress
{
	public static class EntityWrapperExtension
	{
		public static int ReachedLevel(this EntityWrapper e)
		{
			ref var component = ref e.Get<ReachedLevel>();
			return component.Value;
		}

		public static EntityWrapper AddReachedLevel(this EntityWrapper e, int value)
		{
			ref var component = ref e.AddComponent<ReachedLevel>();
			component.Value = value;
			return e;
		}
		
		public static EntityWrapper SetReachedLevel(this EntityWrapper e, int value)
		{
			ref var component = ref e.Get<ReachedLevel>();
			component.Value = value;
			return e;
		}
	}
}