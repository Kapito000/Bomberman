using Gameplay.Feature.Camera.System;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.Camera
{
	public sealed class CameraFeature : Infrastructure.ECS.Feature
	{
		public CameraFeature(ISystemFactory systemFactory) : base(systemFactory)
		{
			AddInit<CreateCameraSystem>();
			AddUpdate<SetCameraFollowSystem>();
		}
	}
}