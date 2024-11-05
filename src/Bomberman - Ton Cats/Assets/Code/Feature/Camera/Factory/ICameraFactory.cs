using Factory;
using UnityEngine;

namespace Feature.Camera.Factory
{
	public interface ICameraFactory : IFactory
	{
		void CreateCamera();
		int CreateVirtualCamera(Transform followTarget);
	}
}