using UnityEngine;

namespace Gameplay.Windows
{
	public abstract class BaseWindow : MonoBehaviour
	{
		WindowId _id;

		public WindowId Id
		{
			get => _id;
			private set => _id = value;
		}

		void Start()
		{
			SetWindowId(ref _id);
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

		protected abstract void SetWindowId(ref WindowId id);

		void Cleanup()
		{
			UnsubscribeUpdates();
			OnCleanup();
		}
	}
}