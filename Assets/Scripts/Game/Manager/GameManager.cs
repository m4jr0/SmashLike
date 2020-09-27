using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isRunning = true;
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Time.timeScale = isRunning ? 1f : 0f;
    }
}
