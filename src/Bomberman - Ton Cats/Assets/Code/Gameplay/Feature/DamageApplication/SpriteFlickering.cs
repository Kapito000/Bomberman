using UnityEngine;

namespace Gameplay.Feature.DamageApplication
{
	public static class SpriteFlickering
	{
		public static void SwitchRenderer(SpriteRenderer renderer)
		{
			renderer.enabled = !renderer.enabled;
		}

		public static void EnableRenderer(SpriteRenderer renderer)
		{
			renderer.enabled = true;
		}
	}
}