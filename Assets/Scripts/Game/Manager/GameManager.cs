using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool IsRunning = true;
    public static GameManager Instance = null;

    void Awake() {
        if (GameManager.Instance == null) {
            GameManager.Instance = this;
        } else if (GameManager.Instance != this) {
            Destroy(gameObject);
        }
    }

    void Update() {
        Time.timeScale = this.IsRunning ? 1f : 0f;
    }
}
