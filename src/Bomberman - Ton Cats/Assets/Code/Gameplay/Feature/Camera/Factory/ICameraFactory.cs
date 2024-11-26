using Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.Feature.Camera.Factory
{
	public interface ICameraFactory : IFactory
	{
		void CreateCamera();
		int CreateVirtualCamera(Transform followTarget);
	}
}