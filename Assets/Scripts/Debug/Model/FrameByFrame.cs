using UnityEngine;

public class FrameByFrame : MonoBehaviour
{
    public Transform target
    {
        get; protected set;
    }

    public bool isEnabled = false;
    public DebugInputManager inputManager;

    private bool m_hasChanged = false;

    void Update()
    {
        CheckIfEnabled();

        if (m_hasChanged)
        {
            GameManager.instance.isRunning = !isEnabled;
            m_hasChanged = false;
        }
        else
        {
            if (!isEnabled || !inputManager.IsNextFrame())
            {
                return;
            }

            GameManager.instance.isRunning = true;
        }
    }

    void FixedUpdate()
    {
        if (isEnabled)
        {
            GameManager.instance.isRunning = false;
        }
    }

    protected virtual void CheckIfEnabled()
    {
        if (!inputManager.IsFrameByFrame())
        {
            return;
        }

        isEnabled = !isEnabled;
        m_hasChanged = true;
    }
}
