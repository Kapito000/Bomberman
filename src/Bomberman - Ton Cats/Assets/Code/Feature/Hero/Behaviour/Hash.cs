using UnityEngine;

namespace Feature.Hero.Behaviour
{
	public static class Hash
	{
		public static readonly int MoveUp = Animator.StringToHash("Move up");
		public static readonly int MoveDown = Animator.StringToHash("Move down");
		public static readonly int MoveLeft = Animator.StringToHash("Move left");
		public static readonly int MoveRight = Animator.StringToHash("Move right");
		public static readonly int Idle = Animator.StringToHash("Idle");
		public static readonly int Death = Animator.StringToHash("Death");		
	}
}