using UnityEngine;

namespace Factory.CameraFactory
{
	public interface ICameraFactory : IFactory
	{
		GameObject CreateCamera();
	}
}