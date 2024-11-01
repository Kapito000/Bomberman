using Infrastructure;

namespace Common.Collisions
{
	public interface ICollisionRegistry : IService
	{
		void Register(int instanceId, int entity);
		void Unregister(int instanceId);
		bool TryGet(int instanceId, out int entity);
	}
}