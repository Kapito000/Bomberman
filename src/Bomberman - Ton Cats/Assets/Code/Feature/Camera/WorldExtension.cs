﻿using Feature.Camera.Component;
using Leopotam.EcsLite;

namespace Feature.Camera
{
	public static class WorldExtension
	{
		public static void AddFollowTarget(this EcsWorld world, int e,
			UnityEngine.Transform target)
		{
			ref var setFollowTarget = ref world.GetPool<FollowTarget>().Add(e);
			setFollowTarget.Value = target;
		}
	}
}