using AB_Utility.FromSceneToEntityConverter;

namespace Infrastructure.ECS
{
	public abstract class Converter<TComponent> : ComponentConverter<TComponent>
		where TComponent : struct
	{ }
}