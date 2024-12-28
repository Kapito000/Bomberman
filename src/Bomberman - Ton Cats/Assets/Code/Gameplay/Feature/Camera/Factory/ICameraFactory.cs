using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.Camera.Factory
{
	public interface ICameraFactory : IFactory
	{
		int CreateCamera();
		int CreateVirtualCamera(Transform followTarget);
	}
}