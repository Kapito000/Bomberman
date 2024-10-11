using System;
using UnityEngine;

namespace Input.Character
{
	public interface ICharacterInput : IInput
	{
		Vector2 Movement { get; }
		event Action PutBomb;
	}
}