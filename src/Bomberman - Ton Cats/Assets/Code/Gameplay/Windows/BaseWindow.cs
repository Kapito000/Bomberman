using UnityEngine;

namespace Gameplay.Windows
{
	public abstract class BaseWindow : MonoBehaviour
	{
		public abstract WindowId Id { get; }

		void Start()
		{
			Initialize();
			SubscribeUpdates();
		}

		void OnDestroy() =>
			Cleanup();

		public virtual void Show()
		{
			gameObject.SetActive(true);
		}

		public virtual void Hide()
		{
			gameObject.SetActive(false);
		}

		protected virtual void Initialize()
		{ }

		protected virtual void SubscribeUpdates()
		{ }

		protected virtual void UnsubscribeUpdates()
		{ }

		protected virtual void OnCleanup()
		{ }

		void Cleanup()
		{
			UnsubscribeUpdates();
			OnCleanup();
		}
	}
}