using Gameplay.Feature.Bomb;
using Gameplay.Feature.Camera;
using Gameplay.Feature.Collisions;
using Gameplay.Feature.DamageApplication;
using Gameplay.Feature.Destruction;
using Gameplay.Feature.Enemy;
using Gameplay.Feature.Explosion;
using Gameplay.Feature.FinishLevel;
using Gameplay.Feature.GameMusic;
using Gameplay.Feature.Hero;
using Gameplay.Feature.HUD;
using Gameplay.Feature.Input;
using Gameplay.Feature.Life;
using Gameplay.Feature.Map;
using Gameplay.Feature.MapGenerator;
using Gameplay.Feature.Timer;
using Gameplay.Feature.UI;
using Infrastructure.Factory.SystemFactory;

namespace Gameplay.Feature
{
	public sealed class GameFeatureController : FeatureController
	{
		public GameFeatureController(ISystemFactory systemFactory)
			: base(systemFactory)
		{
			Add<InputFeature>();
			Add<MapGenerationFeature>();
			Add<CollisionsFeature>();
			Add<HeroFeature>();
			Add<EnemyFeature>();
			Add<BombFeature>();
			Add<ExplosionFeature>();
			Add<DamageApplicationFeature>();
			Add<LifeFeature>();
			Add<MapFeature>();
			Add<TimerFeature>();
			Add<CameraFeature>();
			Add<UiFeature>();
			Add<HudFeature>();
			Add<FinishLevelFeature>();
			Add<GameMusicFeature>();
			Add<DestructionFeature>();
		}
	}
}