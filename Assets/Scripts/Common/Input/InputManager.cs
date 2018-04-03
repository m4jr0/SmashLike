using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public Dictionary<string, string> InputIdDict;

    protected virtual void Initialize() {
        this.InputIdDict = new Dictionary<string, string>();
    }
}
