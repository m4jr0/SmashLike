using System.Collections.Generic;
using UnityEngine;

public class DebugInputManager : PlayerInputManager {
    protected override void InitializeInputIdDicts() {
        base.InitializeInputIdDicts();

        this.InputIdDict = new Dictionary<string, string> {
            { "frame_by_frame", "p{0}_frame_by_frame" },
            { "next_frame", "p{0}_next_frame" }
        };
    }

    public virtual bool IsFrameByFrame() {
        return this.DPadDownDown() ||
               Input.GetButtonDown(this.InputIdDict["frame_by_frame"]);
    }

    public virtual bool IsNextFrame() {
        return this.DPadRightDown() ||
               Input.GetButtonDown(this.InputIdDict["next_frame"]);
    }
}
