using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : InputManager {
    public int PlayerId = 0;
    public Dictionary<string, string> ButtonIdDict;

    private bool _isDPadXInUse = false;
    private bool _isDPadYInUse = false;
    private float _timer = 0f;
    private float _previousTime = 0f;

    void Awake() {
        this.Initialize();
    }

    void Update() {
        this._timer += Time.realtimeSinceStartup - this._previousTime;
        this._previousTime = Time.realtimeSinceStartup;

        if (!(this._timer - Time.fixedDeltaTime > 0f) ||
            !this.CheckDPadAxis()) {
            return;
        }

        this.ResetDPadAxis();
        this._timer = 0f;
    }

    private bool CheckDPadAxis() {
        return this._isDPadXInUse || this._isDPadYInUse;
    }

    private void ResetDPadAxis() {
        this._isDPadXInUse = Mathf.Abs(
            Input.GetAxis(this.ButtonIdDict["dpad_x"])) > 0f;
        this._isDPadYInUse = Mathf.Abs(
            Input.GetAxis(this.ButtonIdDict["dpad_y"])) > 0f;
    }

    protected virtual void InitializeInputIdDicts() {
        this.InputIdDict = new Dictionary<string, string>();

        this.ButtonIdDict = new Dictionary<string, string>() {
            { "dpad_x", "p{0}_dpad_x" },
            { "dpad_y", "p{0}_dpad_y" }
        };
    }

    protected override void Initialize() {
        this.InitializeInputIdDicts();
        this.MapPlayerToInputDict(this.ButtonIdDict);
        this.MapPlayerToInputDict(this.InputIdDict);
    }

    protected virtual void MapPlayerToInputDict(
        Dictionary<string, string> inputDict) {
        // we append the player id to each key
        List<string> keys = new List<string>(inputDict.Keys);

        foreach (string key in keys) {
            inputDict[key] = string.Format(
                inputDict[key],
                this.PlayerId
            );
        }
    }

    public bool DPadRightDown() {
        if (this._isDPadXInUse) return false;

        this._isDPadXInUse = Input.GetAxis(this.ButtonIdDict["dpad_x"]) > 0f;

        return this._isDPadXInUse;
    }

    public bool DPadDownDown() {
        if (this._isDPadYInUse) return false;

        this._isDPadYInUse = Input.GetAxis(this.ButtonIdDict["dpad_y"]) < 0f;

        return this._isDPadYInUse;
    }
}
