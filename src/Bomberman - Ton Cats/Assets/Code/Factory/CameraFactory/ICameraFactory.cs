namespace Factory.CameraFactory
{
	public interface ICameraFactory : IFactory
	{
		int CreateCamera();
		int CreateVirtualCamera();
	}
}