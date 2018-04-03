using UnityEngine;

public class FrameByFrame : MonoBehaviour {
	public Transform Target { get; protected set; }
    public bool IsEnabled = false;
	public DebugInputManager InputManager;

    private bool _hasChanged = false;

    void Update() {
        this.CheckIfEnabled();

        if (this._hasChanged) {
            GameManager.Instance.IsRunning = !this.IsEnabled;

            this._hasChanged = false;
        } else {
            if (!this.IsEnabled) return;

            GameManager.Instance.IsRunning = false;

            if (!this.InputManager.IsNextFrame()) return;

            GameManager.Instance.IsRunning = true;
        }
    }

    protected virtual void CheckIfEnabled() {
        if (!this.InputManager.IsFrameByFrame()) return;

        this.IsEnabled = !this.IsEnabled;
        this._hasChanged = true;
    }
}
