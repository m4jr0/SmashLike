using UnityEngine;
using UnityEngine.UI;

public class DebugViewer : MonoBehaviour
{
    public Text textObject = null;

    // To be overriden in child, if necessary.
    public virtual string label
    {
        get
        {
            return "";
        }
    }

    void Awake()
    {
        textObject = gameObject.GetComponent<Text>();
    }
}
