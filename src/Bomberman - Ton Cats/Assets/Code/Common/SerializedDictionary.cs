using System.Collections.Generic;
using UnityEngine;

namespace Common
{
	public abstract class SerializedDictionary<TKey, TValue> :
		Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField, HideInInspector]
		List<TKey> _keyData = new();

		[SerializeField, HideInInspector]
		List<TValue> _valueData = new();

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			Clear();
			for (int i = 0; i < _keyData.Count && i < _valueData.Count; i++)
			{
				this[_keyData[i]] = _valueData[i];
			}
		}

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			_keyData.Clear();
			_valueData.Clear();

			foreach (var item in this)
			{
				_keyData.Add(item.Key);
				_valueData.Add(item.Value);
			}
		}
	}
}