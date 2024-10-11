using Factory.SystemFactory;
using Feature.Camera.System;

namespace Feature.Camera
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