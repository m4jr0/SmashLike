using UnityEngine;

public class FrameCounter : MonoBehaviour
{
    [HideInInspector]
    public int framePerSecond = -1;

    public int currentFrame
    {
        get; protected set;
    }

    void Awake()
    {
        framePerSecond = (int)(1 / Time.fixedDeltaTime);
        currentFrame = 0;
    }

    void FixedUpdate()
    {
        currentFrame = (currentFrame + 1) % framePerSecond;
    }
}
