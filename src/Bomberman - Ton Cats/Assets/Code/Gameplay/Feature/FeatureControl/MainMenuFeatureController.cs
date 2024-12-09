using Gameplay.Feature.MainMenuMusic;
using Gameplay.Feature.MainMenuUI;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature.FeatureControl
{
	public sealed class MainMenuFeatureController : FeatureController
	{
		public MainMenuFeatureController(ISystemFactory systemFactory)
			: base(systemFactory)
		{
			Add<MainMenuMusicFeature>();
			Add<MainMenuUiFeature>();
		}
	}
}