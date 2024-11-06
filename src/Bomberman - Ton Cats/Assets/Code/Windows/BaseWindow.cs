using UnityEngine;

namespace Windows
{
	public abstract class BaseWindow : MonoBehaviour
	{
		public WindowId Id { get; }

		void Awake() =>
			OnAwake();

		void Start()
		{
			Initialize();
			SubscribeUpdates();
		}

		void OnDestroy() =>
			Cleanup();

		protected virtual void OnAwake()
		{ }

		protected virtual void Initialize()
		{ }

		protected virtual void SubscribeUpdates()
		{ }

		protected virtual void UnsubscribeUpdates()
		{ }

		protected virtual void Cleanup()
		{
			UnsubscribeUpdates();
			OnCleanup();
		}

		protected virtual void OnCleanup()
		{ }

		protected abstract void SetWindowId();
	}
}