using Factory.Kit;
using UnityEngine;

namespace Factory.CameraFactory
{
	public interface ICameraFactory : IFactory
	{
		int CreateCamera();
		int CreateVirtualCamera(Transform followTarget);
	}
}