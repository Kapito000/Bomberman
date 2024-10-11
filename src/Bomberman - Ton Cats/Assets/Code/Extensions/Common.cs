using System;

namespace Extensions
{
	public static class Common
	{
		public static T With<T>(this T obj, Action<T> make)
		{
			make.Invoke(obj);
			return obj;
		}
	}
}