using System;
using Cinemachine;
using UnityEngine;

namespace Feature.Camera
{
	public struct FollowTarget { public Transform Value; }
	[Serializable]
	public struct VirtualCamera { public CinemachineVirtualCamera Value; }
}