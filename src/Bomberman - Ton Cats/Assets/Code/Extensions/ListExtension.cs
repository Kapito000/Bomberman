using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
	public static class ListExtension
	{
		public static T GetRandom<T>(this IList<T> list)
		{
			var index = Random.Range(0, list.Count);
			return list[index];
		}

		public static T GetRandom<T>(this IList<T> list, out int index)
		{
			index = Random.Range(0, list.Count);
			return list[index];
		}
	}
}