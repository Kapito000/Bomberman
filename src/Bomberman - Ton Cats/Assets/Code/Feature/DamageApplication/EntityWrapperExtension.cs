﻿using System.Collections.Generic;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Extensions;
using Feature.DamageApplication.Component;
using UnityEngine;

namespace Feature.DamageApplication
{
	public static class EntityWrapperExtension
	{
		public static EntityWrapper AddToDamageBufferRequest(this EntityWrapper e,
			int otherEntity)
		{
			ref var request = ref e.ReplaceComponent<DamageBufferIncrementRequest>();
			request.Value ??= new List<int>(4);
			if (request.Value.Contains(otherEntity) == false)
				request.Value.Add(otherEntity);
			return e;
		}

		public static EntityWrapper RemoveFromDamageBufferRequest(this EntityWrapper e,
			int otherEntity)
		{
			ref var request = ref e.ReplaceComponent<DamageBufferDecrementRequest>();
			request.Value ??= new List<int>(4);
			if (request.Value.Contains(otherEntity) == false)
				request.Value.Add(otherEntity);
			return e;
		}

		public static List<int> DamageBufferIncrementRequest(this EntityWrapper e)
		{
			ref var request = ref e.Get<DamageBufferIncrementRequest>();
			return request.Value;
		}

		public static List<EcsPackedEntityWithWorld> DamageBuffer(this EntityWrapper e)
		{
			ref var damageBuffer = ref e.Get<DamageBuffer>();
			return damageBuffer.Value;
		}

		public static EntityWrapper ReplaceDamageBuffer(this EntityWrapper e)
		{
			ref var damageBuffer = ref e.ReplaceComponent<DamageBuffer>();
			damageBuffer.Value ??= new List<EcsPackedEntityWithWorld>(4);
			return e;
		}
		
		public static EntityWrapper AddDamageBuffer(this EntityWrapper e)
		{
			ref var damageBuffer = ref e.AddComponent<DamageBuffer>();
			damageBuffer.Value ??= new List<EcsPackedEntityWithWorld>(4);
			return e;
		}
		
		public static EntityWrapper ReplaceToDamageBuffer(this EntityWrapper e,
			int otherEntity)
		{
			ref var damageBuffer = ref e.ReplaceComponent<DamageBuffer>();
			damageBuffer.Value ??= new List<EcsPackedEntityWithWorld>(4);

			if (e.HasInDamageBuffer(otherEntity))
				return e;

			var newPackedOther = e.World().PackEntityWithWorld(otherEntity);
			damageBuffer.Value.Add(newPackedOther);
			return e;
		}

		public static bool HasInDamageBuffer(this EntityWrapper e, int otherEntity)
		{
			var buffer = e.DamageBuffer();
			foreach (var pack in buffer)
			{
				if (pack.Unpack(out var bufferedEntity) &&
				    bufferedEntity == otherEntity)
					return true;
			}

			return false;
		}

		public static List<int> DamageBufferDecrementRequest(this EntityWrapper e)
		{
			ref var removal = ref e.Get<DamageBufferDecrementRequest>();
			return removal.Value;
		}

		public static EntityWrapper RemoveFromDamageBuffer(this EntityWrapper e,
			int otherEntity)
		{
			if (e.Has<DamageBuffer>() == false)
				return e;

			var buffer = e.DamageBuffer();
			for (var i = 0; i < buffer.Count; i++)
			{
				if (buffer[i].Unpack(out var bufferedEntity) &&
				    bufferedEntity == otherEntity)
				{
					buffer.RemoveAt(i);
					return e;
				}
			}
			return e;
		}
	}
}