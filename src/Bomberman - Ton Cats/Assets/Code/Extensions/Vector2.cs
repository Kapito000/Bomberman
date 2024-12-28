namespace Extensions
{
	public static class Vector2
	{
		public static float SqrDistance(UnityEngine.Vector2 a, UnityEngine.Vector2 b)
		{
			float num1 = a.x - b.x;
			float num2 = a.y - b.y;
			return num1 * num1 + num2 * num2;
		}
	}
}