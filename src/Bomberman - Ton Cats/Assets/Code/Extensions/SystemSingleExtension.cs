using UnityEngine;

namespace Extensions
{
	public static class SystemSingleExtension
	{
		public static float Pow(this float value, float p) =>
			Mathf.Pow(value, p);
		
		public static float Pow(this int value, float p) =>
			Mathf.Pow(value, p);
	}
}