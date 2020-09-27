using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Dictionary<string, string> inputIdDict;

    protected virtual void Initialize()
    {
        inputIdDict = new Dictionary<string, string>();
    }
}
