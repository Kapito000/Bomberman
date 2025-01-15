using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
	public static class EnumerableExtensions
	{
		public static Dictionary<T, int> ToIndexDictionary<T>(
			this IEnumerable<T> values)
		{
			return values.Select((obj, idx) => KeyValuePair.Create(obj, idx))
				.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
		}

		public static IEnumerable<T> Do<T>(this IEnumerable<T> source,
			Action<T> action)
		{
			// try more performant way
			if (source is List<T> list)
				list.ForEach(action);

			foreach (var item in source)
			{
				action(item);
			}
			return source;
		}

		public static T MaxBy<T, K>(this IEnumerable<T> source, Func<T, K> compFunc)
			where K : IComparable<K>
		{
			return source.MaxBy(comparer);

			int comparer(T x, T y)
			{
				return compFunc(x).CompareTo(compFunc(y));
			}
		}

		public static T MinBy<T, K>(this IEnumerable<T> source, Func<T, K> compFunc)
			where K : IComparable<K>
		{
			return source.MaxBy(comparer);

			int comparer(T x, T y)
			{
				return compFunc(y).CompareTo(compFunc(x));
			}
		}

		public static T MaxBy<T>(this IEnumerable<T> source, Comparison<T> comparer)
		{
			var maxElement = default(T);
			var hasValue = false;

			foreach (var item in source)
			{
				if (hasValue)
				{
					var comparison = comparer(item, maxElement);
					if (comparison > 0)
						maxElement = item;
				}
				else
				{
					hasValue = true;
					maxElement = item;
				}
			}

			return maxElement;
		}

		public static T MinBy<T>(this IEnumerable<T> source, Comparison<T> comparer)
		{
			var minElement = default(T);
			var hasValue = false;

			foreach (var item in source)
			{
				if (hasValue)
				{
					var comparison = comparer(item, minElement);
					if (comparison < 0)
						minElement = item;
				}
				else
				{
					hasValue = true;
					minElement = item;
				}
			}

			return minElement;
		}

		public static T GetRandom<T>(this IEnumerable<T> source)
		{
			var enumerable = source as T[] ?? source.ToArray();
			var index = UnityEngine.Random.Range(0, enumerable.Length);
			return enumerable.ElementAt(index);
		}
	}
}