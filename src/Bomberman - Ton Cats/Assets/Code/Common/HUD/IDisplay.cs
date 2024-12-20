namespace Common.HUD
{
	public interface IDisplay<in T>
	{
		void SetValue(T value);
	}
}