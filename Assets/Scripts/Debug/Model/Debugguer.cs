using UnityEngine;

public class Debugguer : MonoBehaviour
{
    public bool isEnabled = true;
    public GameObject debuggerContainer = null;

    void Update()
    {
        if (debuggerContainer == null)
        {
            return;
        }

        debuggerContainer.SetActive(isEnabled);
    }
}
