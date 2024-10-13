using Factory;
using UnityEngine;

namespace Feature.Camera.Factory
{
	public interface ICameraFactory : IFactory
	{
		int CreateCamera();
		int CreateVirtualCamera(Transform followTarget);
	}
}