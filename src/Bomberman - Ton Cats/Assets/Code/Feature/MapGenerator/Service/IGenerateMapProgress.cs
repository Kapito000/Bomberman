namespace Feature.MapGenerator.Service
{
	public interface IGenerateMapProgress
	{
		void Report(float value);
	}
}