using System;
using Cinemachine;
using Infrastructure.ECS;

namespace Feature.Camera.Component
{
	[Serializable]
	public struct VirtualCamera
	{
		public CinemachineVirtualCamera Value;
	}

	public sealed class VirtualCameraConverter : Converter<VirtualCamera>
	{ }
}