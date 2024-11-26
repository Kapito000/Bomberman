using System;
using System.Collections.Generic;

namespace Common
{
	public class TypeLocator<TType>
	{
		readonly Dictionary<Type, TType> _dictionary = new();

		public TType this[Type t] => _dictionary[t];

		public TypeLocator<TType> Register(TType instance)
		{
			_dictionary[instance.GetType()] = instance;
			return this;
		}

		public void RegisterRange<T>(IEnumerable<T> range) where T : TType
		{
			foreach (var item in range)
				Register(item);
		}

		public TypeLocator<TType> RegisterAs<T>(TType instance)
			where T : TType
		{
			_dictionary[typeof(T)] = instance;
			return this;
		}

		public TypeLocator<TType> Unregister<T>() where T : TType
		{
			_dictionary.Remove(typeof(T));
			return this;
		}

		public TType Get<T>() where T : TType
		{
			return _dictionary[typeof(T)];
		}
	}
}