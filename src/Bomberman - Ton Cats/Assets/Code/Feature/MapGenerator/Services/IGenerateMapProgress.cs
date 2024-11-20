namespace Feature.MapGenerator.Services
{
	public interface IGenerateMapProgress
	{
		void Report(float value);
	}
}