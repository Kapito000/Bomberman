namespace Infrastructure.ECS
{
	public interface IEntityView
	{
		void SetEntity(int e);
		bool TryGetEntity(out int entity);
	}
}