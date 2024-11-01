using Leopotam.EcsLite;

namespace Extensions
{
	public static class EcsPackedEntityWithWorldExtension
	{
		public static bool Unpack(this EcsPackedEntityWithWorld pack, out int entity)
		{
			return pack.Unpack(out _, out entity);
		}
	}
}