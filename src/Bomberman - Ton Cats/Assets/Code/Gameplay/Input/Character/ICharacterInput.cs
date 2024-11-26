using System;
using UnityEngine;

namespace Gameplay.Input.Character
{
	public interface ICharacterInput : IInput
	{
		Vector2 Movement { get; }
		event Action PutBomb;
	}
}